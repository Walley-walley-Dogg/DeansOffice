using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DeansOffice.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReportsPage.xaml
    /// </summary>
    public partial class ReportsPage : Page
    {
        private readonly DeanDbContext _context = new DeanDbContext();
        public ReportsPage()
        {
            InitializeComponent();
        }

        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            var selected = ReportTypeBox.SelectedItem as ComboBoxItem;
            if (selected == null)
            {
                MessageBox.Show("Выберите тип отчета.");
                return;
            }

            string reportName = selected.Content.ToString();
            string csv = "";

            if (reportName == "Расписание по группе")
                csv = GenerateScheduleReport();
            else if (reportName == "Нагрузка преподавателей")
                csv = GenerateTeacherLoadReport();
            else if (reportName == "Учебный план группы")
                csv = GenerateStudyPlanReport();
            else if (reportName == "Список студентов по группам")
                csv = GenerateStudentListReport();

            if (string.IsNullOrWhiteSpace(csv))
            {
                MessageBox.Show("Ошибка при генерации отчета.");
                return;
            }

            var dialog = new SaveFileDialog
            {
                Filter = "CSV файл (*.csv)|*.csv",
                FileName = $"Отчет_{DateTime.Now:yyyyMMdd_HHmm}.csv"
            };

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, csv, Encoding.UTF8);
                StatusText.Text = "Отчет успешно сохранен.";
            }
        }

        private string GenerateScheduleReport()
        {
            var result = new StringBuilder();
            result.AppendLine("Дата;Время;Предмет;Преподаватель;Группа;Аудитория");

            var data = _context.Schedules
                .Select(s => new
                {
                    s.Date,
                    s.TimeSlot,
                    Subject = s.Subject.SubjectName,
                    Teacher = s.Teacher.LastName + " " + s.Teacher.FirstName.Substring(0, 1) + ".",
                    Group = s.Group.GroupName,
                    s.Room
                })
                .OrderBy(s => s.Date).ThenBy(s => s.TimeSlot)
                .ToList();

            foreach (var row in data)
            {
                result.AppendLine($"{row.Date:dd.MM.yyyy};{row.TimeSlot};{row.Subject};{row.Teacher};{row.Group};{row.Room}");
            }

            return result.ToString();
        }

        private string GenerateTeacherLoadReport()
        {
            var result = new StringBuilder();
            result.AppendLine("Преподаватель;Количество занятий");

            var data = _context.Schedules
                .GroupBy(s => s.Teacher)
                .Select(g => new
                {
                    Teacher = g.Key.LastName + " " + g.Key.FirstName[0] + ".",
                    Count = g.Count()
                }).ToList();

            foreach (var row in data)
            {
                result.AppendLine($"{row.Teacher};{row.Count}");
            }

            return result.ToString();
        }

        private string GenerateStudyPlanReport()
        {
            var result = new StringBuilder();
            result.AppendLine("Группа;Семестр;Предмет;Преподаватель");

            var data = _context.GroupSubjects
                .Join(_context.Groups, gs => gs.GroupID, g => g.GroupID, (gs, g) => new { gs, g })
                .Join(_context.Subjects, gs => gs.gs.SubjectID, s => s.SubjectID, (tmp, s) => new { tmp.gs, tmp.g, Subject = s })
                .ToList();

            foreach (var row in data)
            {
                string teacher = _context.TeacherSubjects
                    .Where(ts => ts.SubjectID == row.gs.SubjectID)
                    .Select(ts => ts.Teacher.LastName)
                    .FirstOrDefault() ?? "—";

                result.AppendLine($"{row.g.GroupName};{row.gs.Semester};{row.Subject.SubjectName};{teacher}");
            }

            return result.ToString();
        }

        private string GenerateStudentListReport()
        {
            var result = new StringBuilder();
            result.AppendLine("Фамилия;Имя;Группа;Email");

            var data = _context.Students
                .Select(s => new
                {
                    s.LastName,
                    s.FirstName,
                    Group = s.Group.GroupName,
                    s.Email
                }).ToList();

            foreach (var row in data)
            {
                result.AppendLine($"{row.LastName};{row.FirstName};{row.Group};{row.Email}");
            }

            return result.ToString();
        }

    }
}
