using ByWoggi.classes;
using System;
using System.Collections.Generic;
using System.IO;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ByWoggi.pages
{
    /// <summary>
    /// Interaction logic for UserProfilePage.xaml
    /// </summary>
    public partial class UserProfilePage : Page
    {
        private UserSession _userSession;
        private MainViewModel _mainViewModel;
        private byte[] _selectedAvatarBytes;
        public UserProfilePage(UserSession userSession)
        {
            InitializeComponent();
            _userSession = userSession;
            _mainViewModel = new MainViewModel(userSession);
            _selectedAvatarBytes = ImageConverter.ImageToByteArray(new BitmapImage(new Uri("pack://application:,,,/images/avatar.png", UriKind.RelativeOrAbsolute)));
            DataContext = _mainViewModel;
        }   

        private void ChooseImageProfile()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*"
            };

            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                string filePath = dialog.FileName;

                _selectedAvatarBytes = File.ReadAllBytes(filePath);

                _mainViewModel.UserProfileImage = new BitmapImage(new Uri(filePath));
                _userSession.CurrentUser.profile_image = _selectedAvatarBytes;
            }
        }


        private void ProfileEditButton_Click(object sender, RoutedEventArgs e)
        {
            switch (ProfileEditing.Visibility)
            {
                case Visibility.Visible:
                    ProfileEditing.Visibility = Visibility.Collapsed;
                    break;
                case Visibility.Collapsed:
                    ProfileEditing.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void ImageChooseAvatar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChooseImageProfile();
        }

        private void ChooseAvatarButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseImageProfile();
        }

        private void SaveUserProfileButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new ByWoggiEntities())
            {
                var currentUser = _userSession.CurrentUser;
                var userService = new UserService(context);

                // Собираем данные для проверки изменений
                string login = txbLogin.Text;
                string email = txbEmail.Text;
                string newPwd = txbNewPwd.Password;
                byte[] newAvatar = _selectedAvatarBytes; // Предположим, что это массив байтов новой аватарки
                bool isAvatarChanged = newAvatar != null && newAvatar.SequenceEqual(currentUser.profile_image);
                if (ProfileEditing.Visibility == Visibility.Visible)
                {
                    // Проверяем изменения
                    bool isLoginChanged = login != currentUser.login && !string.IsNullOrEmpty(login);
                    bool isEmailChanged = email != currentUser.email && !string.IsNullOrEmpty(email);
                    bool isPasswordChanged = !string.IsNullOrEmpty(newPwd) && HashingHelper.HashPassword(newPwd) != currentUser.password;


                    if (!TextboxValidation.areAllTxbsValid(login, newPwd, txbNewPwdAgain.Password, email))
                    {
                        return;
                    }


                    // Если были изменения, обновляем данные
                    if (isLoginChanged || isEmailChanged || isPasswordChanged || isAvatarChanged)
                    {
                        // Если логин или email изменены, выполняем проверки на уникальность
                        if (isLoginChanged && userService.LoginExists(login))
                        {
                            MessageBox.Show("Такой логин уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if (isEmailChanged && userService.EmailExists(email))
                        {
                            MessageBox.Show("Такой E-Mail уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    // Выполняем обновление данных пользователя
                    userService.UpdateUser(currentUser,
                        isLoginChanged ? login : currentUser.login,
                        isEmailChanged ? email : currentUser.email,
                        isPasswordChanged ? HashingHelper.HashPassword(newPwd) : currentUser.password,
                        isAvatarChanged ? newAvatar : currentUser.profile_image
                    );

                    // Обновляем UI через MainViewModel
                    if (isLoginChanged) _mainViewModel.UserLoginDisplay = login;
                    if (isAvatarChanged) 
                    {
                        _mainViewModel.UserProfileImage = ImageConverter.ByteArrayToImage(newAvatar);
                        _mainViewModel.userSession.SignIn(currentUser);
                    }

                }
                else
                {
                    userService.UpdateUser(currentUser,
                    currentUser.login,
                    currentUser.email,
                    currentUser.password,
                    isAvatarChanged ? newAvatar : currentUser.profile_image);
                    _mainViewModel.userSession.SignIn(currentUser);
                }
            }
            MessageBox.Show("Изменения сохранены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

        }

    }

}
