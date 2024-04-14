using ByWoggi.classes;
using ByWoggi.pages;
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

namespace ByWoggi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }


        private void StackPanel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AuthPopup.IsOpen = !AuthPopup.IsOpen;
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationContentFrame.Navigate(new Uri("pages/RegistrationPage.xaml", UriKind.Relative));
        }

        private void LeftHeaderBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RegistrationContentFrame.Content = null;

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new ByWoggiEntities())
            {
                var authService = new AuthService(context);

                // Вход пользователя
                if (authService.AuthenticateUser(LoginTextbox.Text, PasswordTextbox.Password))
                {
                    MessageBox.Show("Вход успешен!");
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Navigate(new Uri("pages/MainContentPage.xaml", UriKind.Relative));

        }
    }
}

