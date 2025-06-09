using DeansOffice.Database.Models;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        private readonly DeanDbContext _context = new DeanDbContext();

        public SchedulePage()
        {
            InitializeComponent();
            LoadData();
            LoadSchedule();
        }

        private void LoadData()
        {
            GroupComboBox.ItemsSource = _context.Groups.ToList();
            SubjectComboBox.ItemsSource = _context.Subjects.ToList();
            TeacherListBox.ItemsSource = _context.Teachers.ToList();
            DatePicker.SelectedDate = DateTime.Today;
        }

        private void LoadSchedule()
        {
            var schedules = _context.Schedules
                .Select(s => new
                {
                    TimeSlot = s.TimeSlot.ToShortDateString(),
                    s.Date,
                    SubjectName = s.Subject.SubjectName,
                    GroupName = s.Group.GroupName,
                    TeacherName = s.Teacher.LastName + " " + s.Teacher.FirstName[0] + ".",
                    s.Room
                }).ToList();

            ScheduleGrid.ItemsSource = schedules;
        }

        private void AddScheduleManual_Click(object sender, RoutedEventArgs e)
        {
            if (GroupComboBox.SelectedItem is Group group &&
                SubjectComboBox.SelectedItem is Subject subject &&
                TeacherListBox.SelectedItem is Teacher teacher &&
                DatePicker.SelectedDate.HasValue &&
                !string.IsNullOrWhiteSpace(TimeBox.Text))
            {
                // Парсим время занятия
                var timeParts = TimeBox.Text.Trim().Split('-');
                if (timeParts.Length != 2 ||
                    !TimeSpan.TryParse(timeParts[0].Trim(), out var startTime) ||
                    !TimeSpan.TryParse(timeParts[1].Trim(), out var endTime))
                {
                    MessageBox.Show("Некорректный формат времени. Используйте формат 'HH:mm - HH:mm'");
                    return;
                }

                var lessonDate = DatePicker.SelectedDate.Value;
                var lessonStart = lessonDate + startTime;
                var lessonEnd = lessonDate + endTime;

                // Проверяем занятость преподавателя
                bool isTeacherBusy = _context.Schedules.Any(s =>
                    s.TeacherID == teacher.TeacherID &&
                    s.TimeSlot.Date == lessonDate.Date &&
                    TimeSpan.Parse(s.Date.Split('-')[0].Trim()) < endTime &&
                    TimeSpan.Parse(s.Date.Split('-')[1].Trim()) > startTime);

                if (isTeacherBusy)
                {
                    MessageBox.Show("Этот преподаватель уже занят в указанное время!");
                    return;
                }

                _context.Schedules.Add(new Schedule
                {
                    GroupID = group.GroupID,
                    SubjectID = subject.SubjectID,
                    TeacherID = teacher.TeacherID,
                    TimeSlot = lessonDate,
                    Date = TimeBox.Text.Trim(),
                    Room = RoomBox.Text
                });

                _context.SaveChanges();
                LoadSchedule();
                MessageBox.Show("Занятие добавлено.");
            }
        }

        private void AutoGenerateSchedule_Click(object sender, RoutedEventArgs e)
        {
            var group = GroupComboBox.SelectedItem as Group;
            var subject = SubjectComboBox.SelectedItem as Subject;

            if (group == null || subject == null)
            {
                MessageBox.Show("Выберите группу и предмет.");
                return;
            }

            var selectedTeachers = TeacherListBox.SelectedItems.Cast<Teacher>().ToList();

            if (selectedTeachers.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы одного преподавателя.");
                return;
            }

            // Настройки
            int maxPairsPerDay = 5;
            int daysInWeek = 5;
            TimeSpan lessonDuration = TimeSpan.FromMinutes(90);
            TimeSpan firstLessonTime = TimeSpan.FromHours(8);
            int maxHoursPerTeacher = 6;

            // Вычисляем общее количество занятий
            int totalLessons = daysInWeek;

            // Составляем список всех возможных временных слотов на неделю
            var allSlots = new List<(DateTime Date, TimeSpan StartTime)>();
            DateTime baseDate = DateTime.Today;

            for (int day = 0; day < daysInWeek; day++)
            {
                for (int slot = 0; slot < maxPairsPerDay; slot++)
                {
                    allSlots.Add((baseDate.AddDays(day),
                                firstLessonTime.Add(TimeSpan.FromMinutes(lessonDuration.TotalMinutes * slot))));
                }
            }

            // Распределение по преподавателям
            Dictionary<int, int> teacherHours = selectedTeachers.ToDictionary(t => t.TeacherID, t => 0);
            int slotIndex = 0;
            int subjectHoursPerLesson = 1;

            for (int lesson = 0; lesson < totalLessons && slotIndex < allSlots.Count; lesson++)
            {
                // Найдем преподавателя с минимальной загрузкой
                var availableTeacher = teacherHours
                    .Where(kvp => kvp.Value + subjectHoursPerLesson <= maxHoursPerTeacher)
                    .OrderBy(kvp => kvp.Value)
                    .FirstOrDefault();

                if (availableTeacher.Key == 0) break;

                var teacherId = availableTeacher.Key;
                var (date, time) = allSlots[slotIndex];
                var endTime = time.Add(lessonDuration);

                // Проверяем занятость преподавателя
                bool isTeacherBusy = _context.Schedules.Any(s =>
                    s.TeacherID == teacherId &&
                    s.TimeSlot.Date == date.Date &&
                    TimeSpan.Parse(s.Date.Split('-')[0].Trim()) < endTime &&
                    TimeSpan.Parse(s.Date.Split('-')[1].Trim()) > time);

                if (isTeacherBusy)
                {
                    // Пропускаем этот слот и пробуем следующий
                    slotIndex++;
                    lesson--; // компенсируем увеличение в for
                    continue;
                }

                _context.Schedules.Add(new Schedule
                {
                    GroupID = group.GroupID,
                    SubjectID = subject.SubjectID,
                    TeacherID = teacherId,
                    TimeSlot = date,
                    Date = time.ToString(@"hh\:mm") + " - " + endTime.ToString(@"hh\:mm"),
                    Room = $"Ауд. {100 + slotIndex}"
                });

                teacherHours[teacherId] += subjectHoursPerLesson;
                slotIndex++;
            }

            _context.SaveChanges();
            LoadSchedule();
            MessageBox.Show("Оптимизированное расписание на неделю создано.");
        }
    }
}
