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
    /// Логика взаимодействия для Teachers.xaml
    /// </summary>
    public partial class Teachers : Page
    {
        private readonly TeacherService _teacherService = new TeacherService(); 
        private List<Teacher> _teachers;
        public Teachers()
        {
            InitializeComponent();
            LoadTeachers();

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTeachers();
        }

        private void LoadTeachers()
        {
            try
            {
                _teachers = _teacherService.GetAllTeachers();
                teachersGrid.ItemsSource = _teachers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки преподавателей: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new TeacherEditDialog(new Teacher());

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _teacherService.AddTeacher(dialog._teacher);
                    LoadTeachers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления преподавателя: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (teachersGrid.SelectedItem is Teacher selectedTeacher)
            {
                var dialog = new TeacherEditDialog(selectedTeacher);

                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        _teacherService.UpdateTeacher(dialog._teacher, selectedTeacher.TeacherID);
                        LoadTeachers();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка обновления преподавателя: {ex.Message}", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите преподавателя для редактирования", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (teachersGrid.SelectedItem is Teacher selectedTeacher)
            {
                var result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить преподавателя {selectedTeacher.FirstName} {selectedTeacher.MiddleName}?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _teacherService.DeleteTeacher(selectedTeacher);
                        LoadTeachers();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка удаления Преподавателя: {ex.Message}", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите преподавателя для удаления", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadTeachers();
        }
    }
}
