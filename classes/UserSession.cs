using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ByWoggi.classes
{
    public class UserSession
    {
        public User CurrentUser { get; private set; }
        public bool IsAuthenticated => CurrentUser != null;

        public event Action OnLoginStatusChanged;

        public void SignIn(User user)
        {
            CurrentUser = user; 
            OnLoginStatusChanged?.Invoke();
        }



        public void SignOut()
        {
            CurrentUser = null; // Очищаем информацию о пользователе при выходе
            OnLoginStatusChanged?.Invoke();
        }

    }
}
    