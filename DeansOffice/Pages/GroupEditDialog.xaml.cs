using DeansOffice.Database.Models;
using DeansOffice.ViewModels;
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
    /// Логика взаимодействия для GroupEditDialog.xaml
    /// </summary>
    public partial class GroupEditDialog : Window
    {
        public Group _group { get; set; }
        public GroupEditViewModel ViewModel { get; } = new GroupEditViewModel();
        public GroupEditDialog(Group group)
        {
            InitializeComponent();
            _group = group;
            this.DataContext = ViewModel;
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //_group = ViewModel.Group;
            DialogResult = true;
            return;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            return;
        }
    }
}