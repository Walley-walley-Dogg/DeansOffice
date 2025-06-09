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
    /// Логика взаимодействия для StudentEditDialog.xaml
    /// </summary>
    public partial class StudentEditDialog : Window
    {
        public Student _student {  get; set; }
        public StudentEditViewModel ViewModel { get; } = new StudentEditViewModel();
        public StudentEditDialog(Student student)
        {
            InitializeComponent();
            _student = student;
            this.DataContext = ViewModel;
            txtFirstName.Text = student.FirstName;
            txtLastName.Text = student.LastName;
            txtMiddleName.Text = student.MiddleName;
            txtEmail.Text = student.Email;
            txtPhoneNumber.Text = student.PhoneNumber;
            txtGroupID.Text =  student.GroupID.ToString();
            txtEnrollmentYear.Text = student.EnrollmentYear.ToString();
            dpBirthDate.Text = student.BirthDate.ToString();
            cbGender.SelectedValue = student.Gender;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Student.FirstName = txtFirstName.Text;
            ViewModel.Student.LastName = txtLastName.Text;
            ViewModel.Student.MiddleName = txtMiddleName.Text;
            ViewModel.Student.Email = txtEmail.Text;
            ViewModel.Student.PhoneNumber = txtPhoneNumber.Text;
            ViewModel.Student.GroupID = Convert.ToInt32(txtGroupID.Text);
            ViewModel.Student.EnrollmentYear = Convert.ToInt32(txtEnrollmentYear.Text);
            ViewModel.Student.BirthDate = Convert.ToDateTime(dpBirthDate.Text);
            ViewModel.Student.Gender = cbGender.Text;
            
            _student = ViewModel.Student;
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
