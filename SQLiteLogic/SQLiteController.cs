using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SQLiteLogic
{
    /// <summary>
    /// Синглтон SQLiteController
    /// </summary>
    public class SQLiteController
    {
        /// <summary>
        /// Экземпляр синглтона SQLiteController
        /// </summary>
        private static SQLiteController _instance;

        /// <summary>
        /// Возвращает экземпляр синглтона класса SQLiteController.
        /// </summary>
        public static SQLiteController Instance => _instance ?? (_instance = new SQLiteController());

        private SQLiteCommand _command;
        private SQLiteCommandsHolder _cHolder;
        private bool _needsInit = true;

        private const bool _DEBUG = true;

        private SQLiteController()
        {
            string db = $"{HttpRuntime.AppDomainAppPath}le.sqlite";
            _command = new SQLiteCommand();
            _cHolder = new SQLiteCommandsHolder();

            bool newDB = !File.Exists(db);
            if (newDB)
                SQLiteConnection.CreateFile(db);
            _needsInit = newDB;

            // try-catch не нужно, поимка будет выше
            SQLiteConnection connection = new SQLiteConnection($"Data Source={db};Version=3;");
            connection.Open();
            _command.Connection = connection;

        }

        public void Init()
        {
            if (!_needsInit)
                return;
            // Начальное создание таблиц и добавление админа
            ExecuteCommand(_cHolder.GetCommand(SQLiteCommandsHolder.Commands.InitUserTable));
            ExecuteCommand(_cHolder.GetCommand(SQLiteCommandsHolder.Commands.InitVRTable));
            ExecuteCommand(_cHolder.GetCommand(SQLiteCommandsHolder.Commands.InitResponseTable));
            string login = "admin";
            string password = AuthController.Instance.MakeHash(login, "Pa$$w0rd");
            AddUser(login, password, "Admin", "", "Super best admin");
        }

        #region Users

        public string GetUserShowname(string login)
        {
            var dt = new DataTable();
            ExecuteCommandReturnable(_cHolder.GetCommand(SQLiteCommandsHolder.Commands.GetUserNameByLogin), dt, ("@u", login));
            if (dt.Rows.Count < 1)
                return null;
            return dt.Rows[0].ItemArray[0].ToString();
        }

        public bool AddUser(string name, string pwd, string type, string description, string showname)
        {
            try
            {
                ExecuteCommand(_cHolder.GetCommand(SQLiteCommandsHolder.Commands.AddUser), ("@u", name), ("@p", pwd), ("@t", type), ("@d", description), ("@n", showname));
                return true;
            }
            catch (Exception e)
            {
                if (_DEBUG)
                {
                    using (StreamWriter sw = new StreamWriter($"{HttpRuntime.AppDomainAppPath}sqlite_debug.txt"))
                    {
                        sw.WriteLine($"[DEBUG] {DateTime.Now.ToString(" dd MMM yyyy, HH:mm", CultureInfo.CurrentCulture)}: AddUser error, {e.Message}. Args: username - {name}, password not shown, type - {type}, description - {description}");
                    }
                }
                return false;
            }
        }

        //public bool IsUserAdmin(string username)
        //{
        //    DataTable dt2 = new DataTable();
        //    ExecuteCommandReturnable(_cHolder.GetCommand(SQLiteCommandsHolder.Commands.GetAuthorLevelByName), dt2, ("@u", username));
        //    string level = dt2.Rows[0].ItemArray[0].ToString();
        //    return level == "su";
        //}


        public string GetPasswordHash(string name)
        {
            var dt = new DataTable();
            ExecuteCommandReturnable(_cHolder.GetCommand(SQLiteCommandsHolder.Commands.CheckUser), dt, ("@u", name));
            if (dt.Rows.Count < 1)
                return null;
            var hasheddb = dt.Rows[0].ItemArray[0].ToString();
            return hasheddb;
        }

        //public string GetAuthorID(string username)
        //{
        //    var dt = new DataTable();
        //    ExecuteCommandReturnable(_cHolder.GetCommand(SQLiteCommandsHolder.Commands.GetAuthorIDByName), dt, ("@u", username));
        //    return dt.Rows[0].ItemArray[0].ToString();
        //}

        #endregion


        #region Commands

        private void ExecuteCommand(string command, params (string, string)[] parameters)
        {
            _command.CommandText = command;
            foreach ((string param, string value) tuple in parameters)
            {
                _command.Parameters.AddWithValue(tuple.param, tuple.value);
            }
            _command.ExecuteNonQuery();
            _command.CommandText = "";
        }

        private void ExecuteCommandReturnable(string command, DataTable dTable, params (string, string)[] parameters)
        {
            _command.CommandText = command;
            foreach ((string param, string value) tuple in parameters)
            {
                _command.Parameters.AddWithValue(tuple.param, tuple.value);
            }

            SQLiteDataAdapter da = new SQLiteDataAdapter(_command);
            da.Fill(dTable);
            _command.CommandText = "";
        }

        #endregion

    }
}
