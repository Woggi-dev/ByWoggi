using ByWoggi.classes;
using ByWoggi.pages;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        private UserSession _userSession;
        private MainViewModel _mainViewModel;
        public MainWindow()
        {
            InitializeComponent();
            _context = new ByWoggiEntities();
            _userSession = new UserSession();
            _mainViewModel = new MainViewModel(_userSession);
            DataContext = _mainViewModel;
            PrintAllGames();    
        }

        private void PrintAllGames()
        {
            _context = new ByWoggiEntities();
            GameListView.ItemsSource = _context.Games.ToList();
        }

        private void SortGames(string categoryName)
        {
            var genreCategoryId = _context.Categories.FirstOrDefault(c => c.name == categoryName)?.category_id;
            GameListView.ItemsSource = _context.Games.Where(g => g.category_id == genreCategoryId).ToList();
        }

        private void RegistrationLogIn_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!_userSession.IsAuthenticated)
            {
                AuthPopup.IsOpen = !AuthPopup.IsOpen;
                return;
            }
            UserPopup.IsOpen = !UserPopup.IsOpen;
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentGameContentPage.Content = null;
            AuthPopup.IsOpen = !AuthPopup.IsOpen;
            RegistrationContentFrame.Navigate(new Uri("pages/RegistrationPage.xaml", UriKind.Relative));
        }

        // Нажатие на главную иконку и логотип лося
        private void LeftHeaderBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RegistrationContentFrame.Content = null;
            CurrentGameContentPage.Content = null;
            GameListView.Visibility = Visibility.Visible;
            PrintAllGames();

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new ByWoggiEntities())
            {
                var authService = new AuthService(_context);
                var user = authService.AuthenticateUser(LoginTextbox.Text, PasswordTextbox.Password);

                if (user == null)
                {
                    MessageBox.Show("Неверный логин или пароль");
                    return;
                }

                _mainViewModel.userSession.SignIn(user);
                RegistrationContentFrame.Content = null;
                AuthPopup.IsOpen = false;

            }


            MessageBox.Show("Вход успешен!");
        }

        private void CurrentGame_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GameListView.Visibility = Visibility.Collapsed;
            var stackPanel = (StackPanel)sender;
            var game = (Game)stackPanel.DataContext;

            var gameContentPage = new CurrentGameContentPage(game.imagePath, game.name, game.description, game.release_date);

            CurrentGameContentPage.Content = gameContentPage;

        }

        private void SortByActionGames(object sender, RoutedEventArgs e)
        {
            SortGames("Экшен");
        }

        private void SortByRPGGames(object sender, RoutedEventArgs e)
        {
            SortGames("RPG");
        }

        private void SortByStrategyGames(object sender, RoutedEventArgs e)
        {
            SortGames("Стратегии");
        }

        private void SortByAdventureGames(object sender, RoutedEventArgs e)
        {
            SortGames("Приключения");
        }

        private void SortBySimulationGames(object sender, RoutedEventArgs e)
        {
            SortGames("Симуляторы");

        }

        private void SortByRaceGames(object sender, RoutedEventArgs e)
        {
            SortGames("Гонки");

        }

        private void SortByIndieGames(object sender, RoutedEventArgs e)
        {
            SortGames("Инди");

        }

        private void SortByCasualGames(object sender, RoutedEventArgs e)
        {
            SortGames("Казуальные");

        }

        private void SortBySportGames(object sender, RoutedEventArgs e)
        {
            SortGames("Спортивные");

        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string searchText = SearchTextbox.Text;
                var game = _context.Games.FirstOrDefault(g => g.name.Contains(searchText));
                if (game != null)
                {
                    GameListView.Visibility = Visibility.Collapsed;
                    CurrentGameContentPage.Navigate(new CurrentGameContentPage(game.imagePath, game.name, game.description, game.release_date));
                }
                else
                {
                    MessageBox.Show("Информация не найдена.");
                }
            }
        }

        private void SearchTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchTextbox.Text = string.Empty;
        }

        private void SearchTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            SearchTextbox.Text = "Поиск...";
        }
    }
}

