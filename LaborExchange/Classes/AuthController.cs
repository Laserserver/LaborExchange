using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace LaborExchange
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

        /// <summary>
        /// Закрытый конструктор класса.
        /// </summary>
        private AuthController()
        {
        }

        public bool IsAuthenticated(string login, string token)
        {
            using (var context = new LaborExchangeEntities())
            {
                var userArray = (from l in context.Logins where l.Username == login select l).ToArray();
                if (userArray.Length != 1)
                    return false;
                string hash = userArray[0].Pass;
                string htnk = MakeToken(login, hash);
                return token == htnk;
            }
        }

        public bool CheckCreds(string login, string password)
        {
            string hash = MakeHash(login, password);
            using(var context = new LaborExchangeEntities())
            {
                var userArray = (from l in context.Logins where l.Username == login select l).ToArray();
                if (userArray.Length != 1)
                    return false;
                return hash == userArray[0].Pass;
            }
        }

        public string MakeToken(string login, string password)
        {
            string hash = MakeHash(login, password);
            var alg = MD5.Create();
            var bytes = Encoding.ASCII.GetBytes(login + hash);
            return Convert.ToBase64String(alg.ComputeHash(bytes));
        }

        private bool AddEmployee(Logins l, List<string> args)
        {

            return false;
        }
        private bool AddCompany(Logins l, List<string> args)
        {

            return false;
        }

        public bool AddUser(string login, string password, string type, List<string> args)
        {
            string hash = MakeHash(login, password);
            using (var context = new LaborExchangeEntities())
            {
                if (Enumerable.Any(context.Logins, contextLogin => contextLogin.Username == login))
                {
                    return false;
                }

                var l = new Logins
                {
                    Username = login,
                    Pass = hash,
                    UserType = type
                };
                context.Logins.Add(l);
                switch (type)
                {
                    case "Company":
                        if (!AddCompany(l, args))
                            return false;
                        break;
                    case "Employee":
                        if (!AddEmployee(l, args))
                            return false;
                        break;
                }
                context.SaveChanges();
                return true;
            }

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