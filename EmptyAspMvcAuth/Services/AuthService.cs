using EmptyAspMvcAuth.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace EmptyAspMvcAuth.Services
{
    public class AuthService
    {
        // such naming looks strange, pick a different one or don't use acronyms
        // Btw. If you are planning to replace it later with real db calls, it will be hard for you to switch
        // because you are no using interfaces
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
                return true; // Don't forget about spaces between code-blocks
            return false;
        }

        public UserData LogIn(RegisterDto registerData)
        {
            var user = _DB.FindUserByLogin(registerData.Login);

            if (!IsPasswordHashesEquals(user, registerData.Password))
                throw new Exception("Password is incorrect"); 
            // You could find more suitable exception type or create a custom one
            // Exception isn't specific enough

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
