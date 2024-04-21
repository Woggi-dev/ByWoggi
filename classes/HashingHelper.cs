using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ByWoggi.classes
{
    public static class HashingHelper
    {
        public static string HashPassword(string password)
        {
            using (var sha512 = SHA512.Create())
            {
                var hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
            }
        }
        public static bool IsHashedWithSHA256(string input)
        {
            // Проверяем, соответствует ли строка длине хеша SHA256
            if (input.Length != 64) return false;

            // Проверяем, состоит ли строка только из допустимых hex-символов
            return input.All(c => (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f'));
        }
    }
}
