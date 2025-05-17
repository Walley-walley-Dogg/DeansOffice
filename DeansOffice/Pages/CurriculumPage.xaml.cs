using DeansOffice.Database;
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
    /// Логика взаимодействия для CurriculumPage.xaml
    /// </summary>
    public partial class CurriculumPage : Page
    {
        private readonly CurriculumService _service = new CurriculumService();
        private List<Group> _allGroups = new List<Group>();
        private List<Subject> _allSubjects = new List<Subject>();
        private List<Teacher> _allTeachers = new List<Teacher>();
        public CurriculumPage()
        {
            InitializeComponent();
            Loaded += CurriculumPage_Loaded;
             LoadAllSubjects();

        }
        private  void LoadAllSubjects()
        {
            try
            {
                _allSubjects =  _service.GetAllSubjects();
                AllSubjectsDataGrid.ItemsSource = _allSubjects;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки дисциплин: {ex.Message}");
            }
        }
        private async void CurriculumPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Загрузка основных данных
                _allGroups = await _service.GetAllGroupsAsync();
                _allSubjects = _service.GetAllSubjects();
                _allTeachers = await _service.GetAllTeachersAsync();

                // Заполнение ComboBox
                GroupsComboBox.ItemsSource = _allGroups;
                GroupsComboBox.DisplayMemberPath = "GroupName";

                SubjectsComboBox.ItemsSource = _allSubjects;
                SubjectsComboBox.DisplayMemberPath = "SubjectName";

                SubjectsByTeacherComboBox.ItemsSource = _allSubjects;
                SubjectsByTeacherComboBox.DisplayMemberPath = "SubjectName";

                SubjectTeachersDataGrid.ItemsSource = _allTeachers;

                SubjectsForGroupsComboBox.ItemsSource= _allSubjects;
                SubjectsForGroupsComboBox.DisplayMemberPath = "SubjectName";



                // Установка начальных значений
                //if (_allGroups.Any()) GroupsComboBox.SelectedIndex = 0;
                //if (_allSubjects.Any()) SubjectsComboBox.SelectedIndex = 0;
                //if(_allTeachers.Any()) GroupsComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // Добавление новой дисциплины
        private async void AddNewSubject_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewSubjectNameTextBox.Text) ||
                !int.TryParse(NewSubjectHoursTextBox.Text, out int hours) ||
                NewSubjectSemesterComboBox.SelectedItem == null)
            {
                MessageBox.Show("Заполните все поля корректно!");
                return;
            }

            try
            {
                var newSubject = new Subject
                {
                    SubjectName = NewSubjectNameTextBox.Text,
                    Hours = hours,
                    Semester = (int)NewSubjectSemesterComboBox.SelectedItem
                };

                await _service.AddSubjectAsync(newSubject);
                LoadAllSubjects();

                // Очистка полей
                NewSubjectNameTextBox.Text = "";
                NewSubjectHoursTextBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления дисциплины: {ex.Message}");
            }
        }

        // Удаление дисциплины
        private async void DeleteSubject_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Tag is int subjectId)
            {
                try
                {
                    await _service.DeleteSubjectAsync(subjectId);
                    LoadAllSubjects();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления дисциплины: {ex.Message}");
                }
            }
        }
        private async void GroupsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GroupsComboBox.SelectedItem is Group selectedGroup)
            {
                try
                {
                    // Загрузка дисциплин для выбранной группы
                    var groupSubjects = await _service.GetGroupSubjectsAsync(selectedGroup.GroupID);
                    GroupSubjectsDataGrid.ItemsSource = groupSubjects;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки дисциплин группы: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private  void SubjectsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (SubjectsComboBox.SelectedItem is Subject selectedSubject)
            //{
            //    try
            //    {
            //        // Загрузка преподавателей для выбранной дисциплины
            //        var subjectTeachers = await _service.GetSubjectTeachersAsync(selectedSubject.SubjectID);
            //        SubjectTeachersDataGrid.ItemsSource = subjectTeachers;
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show($"Ошибка загрузки преподавателей: {ex.Message}", "Ошибка",
            //            MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //}

        }
        private async void SubjectsByTeacherComboBox_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (SubjectsByTeacherComboBox.SelectedItem is Subject selectedSubject)
            {
                try
                {
                    // Загрузка преподавателей для выбранной дисциплины
                    var subjectTeachers = await _service.GetSubjectTeachersAsync(selectedSubject.SubjectID);
                    SubjectTeachersDataGrid.ItemsSource = subjectTeachers;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки преподавателей: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private async void AddSubjectToGroup_Click(object sender, RoutedEventArgs e)
        {
            if (GroupsComboBox.SelectedItem is Group selectedGroup &&
                         SubjectsForGroupsComboBox.SelectedItem is Subject selectedSubject)
            {
                try
                {
                    // Создание новой связи группа-дисциплина
                    var newGroupSubject = new GroupSubject
                    {
                        GroupID = selectedGroup.GroupID,
                        Group = selectedGroup,
                        Subject = selectedSubject,
                        SubjectID = selectedSubject.SubjectID,
                        Semester = selectedSubject.Semester,
                    };

                    // Сохранение в базе
                    await _service.AddGroupSubjectAsync(newGroupSubject);

                    // Обновление отображения
                    GroupsComboBox_SelectionChanged(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления дисциплины: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private  void AddTeacherToSubject_Click(object sender, RoutedEventArgs e)
        {
            //if (SubjectsComboBox.SelectedItem is Subject selectedSubject &&
            //    _allTeachers.Any())
            //{
            //    try
            //    {
            //        // Создание новой связи дисциплина-преподаватель
            //        var newTeacherSubject = new TeacherSubject
            //        {
            //            SubjectID = selectedSubject.SubjectID,
            //            Subject = selectedSubject,
            //            Teacher = _allTeachers.First(),
            //            TeacherID = _allTeachers.First().TeacherID
            //        };

            //        // Сохранение в базе
            //        await _service.AddTeacherSubjectAsync(newTeacherSubject);

            //        // Обновление отображения
            //        SubjectsComboBox_SelectionChanged(null, null);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show($"Ошибка добавления преподавателя: {ex.Message}", "Ошибка",
            //            MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //}
            SubjectTeachersDataGrid.ItemsSource = _allTeachers;
            AddTeacherToSubjectOk_btn.Visibility = Visibility.Visible;
        }
        private async void AddTeacherToSubjectOk_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectsComboBox.SelectedItem is Subject selectedSubject && SubjectTeachersDataGrid.SelectedItem is Teacher selectedTeacher)
            {
                try
                {
                    // Создание новой связи дисциплина-преподаватель
                    var newTeacherSubject = new TeacherSubject
                    {
                        SubjectID = selectedSubject.SubjectID,
                        Subject = selectedSubject,
                        Teacher = selectedTeacher,
                        TeacherID = selectedTeacher.TeacherID,
                    };

                    // Сохранение в базе
                    await _service.AddTeacherSubjectAsync(newTeacherSubject);

                    // Обновление отображения
                    SubjectsComboBox_SelectionChanged(null, null);
                    AddTeacherToSubjectOk_btn.Visibility= Visibility.Collapsed;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления преподавателя: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
       



    }
}
