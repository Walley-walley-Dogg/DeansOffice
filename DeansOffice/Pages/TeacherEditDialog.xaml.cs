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
    /// Логика взаимодействия для TeacherEditDialog.xaml
    /// </summary>
    public partial class TeacherEditDialog : Window
    {
        public Teacher _teacher {  get; set; }
        public TeacherEditDialogViewModel ViewModel { get; } = new TeacherEditDialogViewModel();

        public TeacherEditDialog(Teacher teacher)
        {
            InitializeComponent();
            _teacher = teacher;
            this.DataContext = ViewModel;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _teacher = ViewModel.teacher;
            ViewModel.teacher.FirstName = txtFirstName.Text;
            ViewModel.teacher.LastName = txtLastName.Text;
            ViewModel.teacher.MiddleName = txtMiddleName.Text;
            ViewModel.teacher.PhoneNumber = txtPhoneNumber.Text;
            ViewModel.teacher.Email = txtPhoneNumber.Text;
            ViewModel.teacher.AcademicTitle = cbAcademicTitle.Text;
            ViewModel.teacher.Department = cbDepartment.Text;

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
