using DeansOffice.Database.Models;
using DeansOffice.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeansOffice.Pages
{
    /// <summary>
    /// Логика взаимодействия для StudentsPage.xaml
    /// </summary>
    public partial class StudentsPage : Page
    {
        private readonly StudentService _studentService = new StudentService();
        private List<Student> _students;

        public StudentsPage()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadStudents();
        }

        private void LoadStudents()
        {
            try
            {
                _students = _studentService.GetAllStudents();
                studentsGrid.ItemsSource = _students;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки студентов: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StudentEditDialog(new Student
            {
                BirthDate = DateTime.Now.AddYears(-18),
                EnrollmentYear = DateTime.Now.Year
            });

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _studentService.AddStudent(dialog._student);
                    LoadStudents();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления студента: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (studentsGrid.SelectedItem is Student selectedStudent)
            {
                var dialog = new StudentEditDialog(selectedStudent);

                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        _studentService.UpdateStudent(dialog._student, selectedStudent.StudentID);
                        LoadStudents();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка обновления студента: {ex.Message}", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите студента для редактирования", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (studentsGrid.SelectedItem is Student selectedStudent)
            {
                var result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить студента {selectedStudent.LastName} {selectedStudent.FirstName}?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _studentService.DeleteStudent(selectedStudent);
                        LoadStudents();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка удаления студента: {ex.Message}", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите студента для удаления", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadStudents();
        }

    }
}
