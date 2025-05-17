using DeansOffice.Database.Models;
using DeansOffice.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;
using static DeansOffice.ViewModels.MainViewModel;

namespace DeansOffice.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        private Teacher current_admin {  get; set; }
        public MainPage(Teacher admin)
        {
            InitializeComponent();
            current_admin = admin;
            NavigationService.MainFrame = MainContentFrame;
            DataContext = new MainViewModel();
        }

        private void Exit_btn_click(object sender, RoutedEventArgs e)
        {
            var loginPage = new LoginPage();
            loginPage.Show();
            this.Close();
        }
    }
    //public class BoolToVisibilityConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (value is bool boolValue)
    //        {
    //            bool invert = parameter != null && bool.TryParse(parameter.ToString(), out bool param) && param;

    //            if (invert) boolValue = !boolValue;

    //            return boolValue ? Visibility.Visible : Visibility.Collapsed;
    //        }
    //        return Visibility.Collapsed;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
