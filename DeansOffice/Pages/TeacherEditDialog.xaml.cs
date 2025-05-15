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
