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

            if (!TextboxValidation.areAllTxbsValid(login, pwd, PasswordTextboxRetry.Password, email))
            {
                return;
            }

            // Проверка валидности успешна - процесс регистрации
            using (var context = new ByWoggiEntities())
            {
                var authService = new UserService(context);

                if (authService.RegisterUser(login, pwd, email, ImageConverter.ImageToByteArray(new BitmapImage(new Uri("pack://application:,,,/images/avatar.png", UriKind.RelativeOrAbsolute)))))
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
