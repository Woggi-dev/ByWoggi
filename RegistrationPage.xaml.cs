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
            // Проверка валидности логина
            if (!Regex.IsMatch(LoginTextBox.Text, @"^[a-zA-Z0-9]{3,}$"))
            {
                MessageBox.Show("Логин должен содержать минимум 3 символа, без пробелов, русских букв и спецсимволов");
                return;
            }
            // Проверка валидности пароля
            else if (!Regex.IsMatch(PasswordTextbox.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{7,}$"))
            {
                MessageBox.Show("Пароль должен содержать минимум 7 символов с заглавными и маленькими буквами и спецсимволами, без пробелов и русских букв!");
                return;
            }
            // Проверка идентичности паролей
            else if (PasswordTextbox.Password != PasswordTextboxRetry.Password)
            {
                MessageBox.Show("Пароли должны совпадать!");
                return;

            }
            // Проверка валидность Email
            else if (!Regex.IsMatch(EmailTextBox.Text, @"^[^@\s]+@(yandex\.ru|mail\.ru|gmail\.com)$"))
            {
                MessageBox.Show("Невалидный Email!");
                return;
            }

            MessageBox.Show("Регистрация успешна!");
        }

    }
}
