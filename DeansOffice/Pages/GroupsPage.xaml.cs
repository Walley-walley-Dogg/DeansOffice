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
    /// Логика взаимодействия для GroupsPage.xaml
    /// </summary>
    public partial class GroupsPage : Page
    {
        private readonly GroupService _groupService = new GroupService();
        private List<Group> _groups;
        public GroupsPage()
        {
            InitializeComponent();
            LoadGroups();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGroups();
        }
        private void LoadGroups()
        {
            try
            {
                _groups = _groupService.GetAllGroups();
                GroupsGrid.ItemsSource = _groups;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки групп: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new GroupEditDialog(new Group());

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _groupService.AddGroup(dialog._group);
                    LoadGroups();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления группы: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (GroupsGrid.SelectedItem is Group selectedGroup)
            {
                var dialog = new GroupEditDialog(selectedGroup);

                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        _groupService.UpdateGroup(dialog._group, selectedGroup.GroupID);
                        LoadGroups();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка обновления группы: {ex.Message}", "Ошибка",
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
            if (GroupsGrid.SelectedItem is Group selectedGroup)
            {
                var result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить группу {selectedGroup.GroupName} ?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _groupService.DeleteGroup(selectedGroup);
                        LoadGroups();
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
            LoadGroups();
        }

    }
}
