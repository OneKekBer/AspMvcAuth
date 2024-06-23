using EmptyAspMvcAuth.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace EmptyAspMvcAuth.Services
{
    public class AuthService
    {
        DataBaseService _DB { get; init; }

        public AuthService(DataBaseService dataBase)
        {
            _DB = dataBase;
        }

        private static string ConvertPasswordToHash(string password)
        {
            return Encoding.UTF8.GetString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
        }

        private static bool IsPasswordHashesEquals(UserData user, string incomePassword)
        {
            if (user.PasswordHash == ConvertPasswordToHash(incomePassword))
                return true;
            return false;
        }

        public UserData LogIn(RegisterDto registerData)
        {
            var user = _DB.FindUserByLogin(registerData.Login);

            if (!IsPasswordHashesEquals(user, registerData.Password))
                throw new Exception("Password is incorrect");

            return user;
        }

        public UserData RegisterUser(RegisterDto registerData)
        {
            if (_DB.IsLoginExists(registerData.Login))
                throw new Exception("Login already used");

            var createdUser = new UserData(registerData.Login, ConvertPasswordToHash(registerData.Password));
            _DB.users.Add(createdUser);
            return createdUser;
        }
    }
}
