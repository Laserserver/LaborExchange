using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteLogic
{
    /// <summary>
    /// Синглтон AuthController
    /// </summary>
    public class AuthController
    {
        /// <summary>
        /// Экземпляр синглтона AuthController
        /// </summary>
        private static AuthController _instance;

        /// <summary>
        /// Возвращает экземпляр синглтона класса AuthController.
        /// </summary>
        public static AuthController Instance => _instance ?? (_instance = new AuthController());

        private RNGCryptoServiceProvider _rngc = new RNGCryptoServiceProvider();
        /// <summary>
        /// Закрытый конструктор класса.
        /// </summary>
        private AuthController()
        {

        }

        public bool IsAuthenticated(string login, string token)
        {
            string hash = SQLiteController.Instance.GetPasswordHash(login);
            string htnk = MakeToken(login, hash);
            return token == htnk;
        }

        public bool CheckCreds(string login, string password)
        {
            string hash = MakeHash(login, password);
            string fromDB = SQLiteController.Instance.GetPasswordHash(login);
            return hash == fromDB;
        }

        public string MakeToken(string login, string password)
        {
            string hash = MakeHash(login, password);
            var alg = MD5.Create();
            var bytes = Encoding.ASCII.GetBytes(login + hash);
            return Convert.ToBase64String(alg.ComputeHash(bytes));
        }

        public bool AddUser(string login, string password, string type, string name)
        {
            string hash = MakeHash(login, password);

            return SQLiteController.Instance.AddUser(login, hash, type, "", name);
        }

        public string MakeHash(string login, string password)
        {
            var alg = MD5.Create();
            var bytes = Encoding.ASCII.GetBytes(login);
            var t = Convert.ToBase64String(alg.ComputeHash(bytes));
            var nbytes = Encoding.ASCII.GetBytes(t);
            var salt = new byte[16];
            int k = 0;
            for (int i = 0; i < nbytes.Length; i++)
            {
                salt[k++] = nbytes[i];
                if (i == nbytes.Length)
                    i = 0;
                if (k == 16)
                    break;
            }

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

    }
}
