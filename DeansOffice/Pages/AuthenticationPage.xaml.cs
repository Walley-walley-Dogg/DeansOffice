using DeansOffice.Database;
using DeansOffice.Utils;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeansOffice.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthenticationPage.xaml
    /// </summary>
    public partial class AuthenticationPage : Window
    {
        public AuthenticationPage()
        {
            InitializeComponent();
            
            
        }


        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите фамилию");
                return;
            }

            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите имя");
                return;
            }

            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите email");
                return;
            }

            if (string.IsNullOrWhiteSpace(PhoneTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите телефон");
                return;
            }
            if(!EmailValidation.is_good(EmailTextBox.Text.ToString().Trim()))
            {
                MessageBox.Show("Введите корректную электронную почту.", "Ошибка");
                return;
            }
            if(!PhoneValidation.is_good(PhoneTextBox.Text.ToString().Trim()))
            {
                MessageBox.Show("Введите корректный номер телефона (пример: +79991234567).", "Ошибка");
                return;
            }
            if (PasswordBox.Password != ConfirmPasswordBox.Password)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }

            if (PasswordBox.Password.Length < 5)
            {
                MessageBox.Show("Пароль должен содержать минимум 5 символов");
                return;
            }

            if (DepartmentComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Пожалуйста, выберите кафедру");
                return;
            }

            if (TitleComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Пожалуйста, выберите звание");
                return;
            }
            
            try
            {
                bool reg = DatabaseQueries.RegisterNewUser(
                    LastNameTextBox.Text,
                    FirstNameTextBox.Text,
                    MiddleNameTextBox.Text,
                    EmailTextBox.Text,
                    PhoneTextBox.Text,
                    PasswordBox.Password,
                    DepartmentComboBox.SelectedValue.ToString(),
                    TitleComboBox.SelectedValue.ToString()
                );
                if( !reg )
                {
                    MessageBox.Show("Почта уже используется!");
                    return;
                }
                MessageBox.Show("Регистрация прошла успешно!");
                LoginPage loginPage = new LoginPage();
                loginPage.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации: {ex.Message}");
            }
        }

        private async void LoginLink_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.BeginAnimation(OpacityProperty, new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.2)));
            await Task.Delay(200);

            LoginPage loginPage = new LoginPage();
            loginPage.Show();

            this.Close();
        }
    }
}
