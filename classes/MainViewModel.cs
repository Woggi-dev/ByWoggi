using System;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ByWoggi.classes
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public UserSession userSession; 

        // Свойство для отображения имени пользователя или кнопок "Войти/Регистрация"
        private string _userLoginDisplay;
        private BitmapImage _userProfileImage;

        public string UserLoginDisplay
        {
            get { return _userLoginDisplay; }
            set
            {
                _userLoginDisplay = value;
                OnPropertyChanged(nameof(UserLoginDisplay));
            }
        }

        public BitmapImage UserProfileImage
        {
            get { return _userProfileImage; }
            set
            {
                _userProfileImage = value;
                OnPropertyChanged(nameof(UserProfileImage));
            }
        }

        // Конструктор
        public MainViewModel(UserSession userSession)
        {
            // Создаем сессию пользователя и подписываемся на событие изменения статуса входа
            this.userSession = userSession;
            this.userSession.OnLoginStatusChanged += UpdateUI;
            UpdateUI();
        }

        // Метод для обновления интерфейса
        private void UpdateUI()
        {
            if (userSession.IsAuthenticated)
            {
                UserLoginDisplay = userSession.CurrentUser.login; // Имя пользователя для отображения
                UserProfileImage = ImageConverter.ByteArrayToImage(userSession.CurrentUser.profile_image);
            }
            else
            {
                UserLoginDisplay = "Авторизация"; // Текст кнопок для отображения
                UserProfileImage = new BitmapImage(new Uri("/images/avatar.png", UriKind.RelativeOrAbsolute));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
