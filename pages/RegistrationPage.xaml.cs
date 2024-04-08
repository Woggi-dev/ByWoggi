using ByWoggi.classes;
using ByWoggi.pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ByWoggi
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }


        private void FullyRegButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string pwd = PasswordTextbox.Password;
            string email = EmailTextBox.Text;
            // Проверка валидности логина
            if (!Regex.IsMatch(login, @"^[a-zA-Z0-9]{3,}$"))
            {
                MessageBox.Show("Логин должен содержать минимум 3 символа, без пробелов, русских букв и спецсимволов");
                return;
            }
            // Проверка валидности пароля
            else if (!Regex.IsMatch(pwd, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{7,}$"))
            {
                MessageBox.Show("Пароль должен содержать минимум 7 символов с заглавными и маленькими буквами и спецсимволами, без пробелов и русских букв!");
                return;
            }
            // Проверка идентичности паролей
            else if (pwd != PasswordTextboxRetry.Password)
            {
                MessageBox.Show("Пароли должны совпадать!");
                return;

            }
            // Проверка валидность Email
            else if (!Regex.IsMatch(email, @"^[^@\s]+@(yandex\.ru|mail\.ru|gmail\.com)$"))
            {
                MessageBox.Show("Невалидный Email!");
                return;
            }

            // Проверка валидности успешна - процесс регистрации
            using (var context = new ByWoggiEntities())
            {
                var authService = new AuthService(context);

                if (authService.RegisterUser(login, pwd, email))
                {
                    MessageBox.Show("Регистрация успешна!");
                }
                else
                {
                    MessageBox.Show("Пользователь с таким логином или email уже существует");
                }
            }



        }

    }
}
