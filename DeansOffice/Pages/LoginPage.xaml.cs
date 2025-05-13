using DeansOffice.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DeansOffice.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите почту");
                return;
            }
            if(PasswordBox.Password.Length < 5)
            {
                MessageBox.Show("Пароль должен содержать минимум 5 символов");
                return;
            }

            try
            {
                DatabaseQueries.Login(
                    EmailTextBox.Text,
                    PasswordBox.Password
                  
                );

                MessageBox.Show("Вход прошел успешно!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка Входа: {ex.Message}");
            }

        }

        private async void RegisterLink_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
            this.BeginAnimation(OpacityProperty, new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.2)));
            await Task.Delay(200);

            AuthenticationPage authenticationPage = new AuthenticationPage();
            authenticationPage.Show();

            this.Close();
        }
    }
}
