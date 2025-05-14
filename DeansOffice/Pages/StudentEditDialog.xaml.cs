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
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //_student = ViewModel.Student;
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
