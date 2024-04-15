using ByWoggi.classes;
using ByWoggi.pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private ByWoggiEntities _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = new ByWoggiEntities();
            GameListView.ItemsSource = _context.Games.ToList();
        }


        private void RegistrationLogIn_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AuthPopup.IsOpen = !AuthPopup.IsOpen;
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentGameContentPage.Content = null;
            RegistrationContentFrame.Navigate(new Uri("pages/RegistrationPage.xaml", UriKind.Relative));
        }

        // Нажатие на главную иконку и логотип лося
        private void LeftHeaderBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RegistrationContentFrame.Content = null;
            CurrentGameContentPage.Content = null;
            GameListView.Visibility = Visibility.Visible;

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

        private void CurrentGame_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GameListView.Visibility = Visibility.Collapsed;
            var stackPanel = (StackPanel)sender;
            var game = (Game)stackPanel.DataContext;

            // Создаем экземпляр страницы с параметрами
            var gameContentPage = new CurrentGameContentPage(game.imagePath, game.name, game.description, game.release_date);

            // Теперь устанавливаем созданный экземпляр страницы в Frame
            CurrentGameContentPage.Content = gameContentPage; // CurrentGameContentFrame это имя Frame в вашем XAML

        }
    }
}

