using System;
using System.Security.Cryptography;
using System.Text;

namespace OnlineBanking.Security
{
    public static class PasswordEncoder
    {
        public static String GetHash(string password)
        {
            password = AddSalt(password);
            return HashPassword(password);
        }
        private static String AddSalt(string password) => password + "verySecureSalt!";
        private static string HashPassword(string password)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
