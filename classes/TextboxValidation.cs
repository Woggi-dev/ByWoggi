using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace ByWoggi.classes
{
    static class TextboxValidation
    {
        public static bool IsLoginValid(string login)
        {
            return Regex.IsMatch(login, @"^[a-zA-Z0-9]{3,}$");

        }
        public static bool IsPwdValid(string pwd)
        {
            return Regex.IsMatch(pwd, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{7,}$");
        }
        public static bool ArePwdsEqual(string pwd, string pwdAgain)
        {
            return pwd == pwdAgain;
        }

        public static bool isEmailValid(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@(yandex\.ru|mail\.ru|gmail\.com)$");
        }

        public static bool areAllTxbsValid(string login, string pwd, string pwdAgain, string email)
        {
            bool[] txbValidations =
            {
                IsLoginValid(login),
                IsPwdValid(pwd),
                ArePwdsEqual(pwd, pwdAgain),
                isEmailValid(email)
            };
            string[] errorMessages =
            {
                "Логин должен содержать минимум 3 символа, без пробелов, русских букв и спецсимволов",
                "Пароль должен содержать минимум 7 символов с заглавными и маленькими буквами и спецсимволами, без пробелов и русских букв!",
                "Пароли должны совпадать!",
                 "Невалидный Email!"
            };
            for (int i = 0; i < txbValidations.Length; i++)
            {
                if (!txbValidations[i])
                {
                    MessageBox.Show(errorMessages[i], "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return true;

        }
    }
}
