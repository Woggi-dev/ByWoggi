using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByWoggi.classes
{
    public class UserService
    {
        private readonly DbContext _context;

        public UserService(DbContext context)
        {
            _context = context;
        }

        public bool EmailExists(string email)
        {
            return _context.Set<User>().Any(e => e.email == email);
        }

        public bool LoginExists(string login)
        {
            return _context.Set<User>().Any(l => l.login == login);
        }

        public void UpdateUser(User userToUpdate, string newLogin, string newEmail, string newPwd, byte[] profileImage = null)
        {
            var existingUser = _context.Set<User>().FirstOrDefault(u => u.user_id == userToUpdate.user_id);

            existingUser.login = newLogin;
            existingUser.email = newEmail;
            existingUser.password = newPwd;
            existingUser.profile_image = profileImage;
            userToUpdate.login = newLogin;
            userToUpdate.email = newEmail;
            userToUpdate.password = newPwd;
            userToUpdate.profile_image = profileImage;
            

            _context.SaveChanges();
        }

        public bool RegisterUser(string login, string password, string email, byte[] profileImage = null)
        {
            if (!(LoginExists(login) || EmailExists(email)))
            {
                var newUser = new User
                {
                    login = login,
                    password = HashingHelper.HashPassword(password),
                    email = email,
                    profile_image = profileImage
                };

                _context.Set<User>().Add(newUser);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
        public User AuthenticateUser(string login, string password)
        {
            var user = _context.Set<User>().FirstOrDefault(u => u.login == login);

            if (user != null && HashingHelper.HashPassword(password) == user.password)
            {
                return user;
            }

            return null;
        }
    }
}
