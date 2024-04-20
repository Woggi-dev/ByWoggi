using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ByWoggi.classes
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public UserSession userSession; 

        // Свойство для отображения имени пользователя или кнопок "Войти/Регистрация"
        private string _userDisplay;

        public string UserDisplay
        {
            get { return _userDisplay; }
            set
            {
                _userDisplay = value;
                OnPropertyChanged(nameof(UserDisplay));
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
                UserDisplay = userSession.CurrentUser.login; // Имя пользователя для отображения
            }
            else
            {
                UserDisplay = "Авторизация"; // Текст кнопок для отображения
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
