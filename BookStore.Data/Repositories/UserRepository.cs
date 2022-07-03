using BookStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BookStore.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DbCon _context;
        public UserRepository(DbCon context)
        {
            _context = context;

            if (!_context.Users.Any())
            {

                _context.Users.Add(new User
                {
                    Id = "1800120",
                    FullName = "Nguyễn Lập An Khương",
                    UserName = "khuongcutedulam",
                    Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
                    FavoriteColor = "Red",
                    Role = "Admin",
                    GoogleId = "123"
                });
                _context.SaveChanges();
            }
        }

        public User GetByUsernameAndPassword(string UserName, string Password)
        {
            var hashPWD = ComputeSha256Hash(Password);
            return _context.Users.FirstOrDefault(x => x.UserName == UserName && x.Password == hashPWD);
        }

        public User GetByGoogleIndentifier(string GoogleId)
        {
            return _context.Users.FirstOrDefault(x => x.GoogleId == GoogleId);
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
    public interface IUserRepository
    {
        User GetByUsernameAndPassword(string Username, string Password);
        User GetByGoogleIndentifier(string GoogleId);
    }
}
