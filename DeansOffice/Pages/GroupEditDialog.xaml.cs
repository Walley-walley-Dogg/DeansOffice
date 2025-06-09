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
            txtGroupName.Text = group.GroupName;
            txtCourse.Text = group.Course.ToString();
            txtSpeciality.Text = group.Specialty.ToString();
            txtCuratorID.Text = group.CuratorID.ToString();

        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Group.GroupName = txtGroupName.Text;
            ViewModel.Group.Course = Convert.ToInt32(txtCourse.Text);
            ViewModel.Group.Specialty = txtSpeciality.Text;
            ViewModel.Group.CuratorID = Convert.ToInt32(txtCuratorID.Text);


            _group = ViewModel.Group;
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