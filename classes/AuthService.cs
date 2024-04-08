﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByWoggi.classes
{
    public class AuthService
    {
        private readonly DbContext _context;

        public AuthService(DbContext context)
        {
            _context = context;
        }

        public bool RegisterUser(string login, string password, string email)
        {
            if (_context.Set<User>().Any(u => u.login == login))
            {
                // Пользователь с таким логином уже существует
                return false;
            }
            if (_context.Set<User>().Any(u => u.email == email))
            {
                // Пользователь с такой почтой уже существует
                return false;
            }

            var newUser = new User
            {
                login = login,
                password = HashingHelper.HashPassword(password), // Хешируем пароль перед сохранением
                email = email
            };

            _context.Set<User>().Add(newUser);
            _context.SaveChanges();

            return true;
        }
        public bool AuthenticateUser(string login, string password)
        {
            var user = _context.Set<User>().FirstOrDefault(u => u.login == login);
            if (user == null)
            {
                // Пользователь не найден
                return false;
            }

            // Проверяем, совпадает ли хеш введённого пароля с хешем из базы данных
            return HashingHelper.HashPassword(password) == user.password;
        }
    }
}