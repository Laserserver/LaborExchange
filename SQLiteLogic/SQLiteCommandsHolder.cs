using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteLogic
{
    public class SQLiteCommandsHolder
    {
        public enum Commands
        {
            InitUserTable,
            InitVRTable,
            InitResponseTable,
            InitReportTable,

            /// <summary>
            /// @u username, @p password, @n show name, @t type, @d description
            /// </summary>
            AddUser,
            /// <summary>
            /// @u username
            /// </summary>
            CheckUser,
            GetUserNameByLogin
        }

        private readonly Dictionary<Commands, string> _commands;

        public SQLiteCommandsHolder()
        {
            _commands = new Dictionary<Commands, string>
            {
                //{ Commands.InitArticlesTable, "CREATE TABLE IF NOT EXISTS Articles (id INTEGER PRIMARY KEY AUTOINCREMENT, header TEXT UNIQUE, content TEXT, creation TEXT, author INTEGER, FOREIGN KEY(author) REFERENCES Users(id))"},
                { Commands.InitUserTable, "CREATE TABLE IF NOT EXISTS UserTable (" +
                                          "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                          "login TEXT UNIQUE NOT NULL, " +
                                          "name TEXT NOT NULL, " +
                                          "pass TEXT NOT NULL, " +
                                          "type TEXT NOT NULL, " +
                                          "description TEXT)"},
                { Commands.InitVRTable, "CREATE TABLE IF NOT EXISTS VRTable (" +
                                        "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                        "authorid INTEGER NOT NULL, " +
                                        "title TEXT NOT NULL, " +
                                        "description TEXT NOT NULL, " +
                                        "creation TEXT NOT NULL, " +
                                        "FOREIGN KEY (authorid) " +
                                        "REFERENCES UserTable(id) " +
                                        "ON UPDATE CASCADE " +
                                        "ON DELETE CASCADE)"},
                { Commands.InitReportTable, "CREATE TABLE IF NOT EXISTS ReportTable (" +
                                        "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                        "authorid INTEGER NOT NULL, " +
                                        "targetid TEXT NOT NULL, " +
                                        "type TEXT NOT NULL, " +
                                        "report TEXT NOT NULL, " +
                                        "creation TEXT NOT NULL, " +
                                        "FOREIGN KEY (authorid) " +
                                        "REFERENCES UserTable(id) " +
                                        "ON UPDATE CASCADE " +
                                        "ON DELETE CASCADE)"},
                { Commands.InitResponseTable, "CREATE TABLE IF NOT EXISTS ResponseTable (" +
                                              "viewerid INTEGER NOT NULL, " +
                                              "vrid INTEGER NOT NULL, " +
                                              "response TEXT, " +
                                              "viewtime TEXT NOT NULL, " +
                                              "FOREIGN KEY(viewerid) REFERENCES UserTable(id)"+
                                              "ON UPDATE CASCADE " +
                                              "ON DELETE CASCADE, " +
                                              "FOREIGN KEY(vrid) REFERENCES VRTable(id)" +
                                              "ON UPDATE CASCADE " +
                                              "ON DELETE CASCADE, " +
                                              "PRIMARY KEY (viewerid, vrid, viewtime))"},
                { Commands.AddUser, "INSERT INTO UserTable (login, pass, name, type, description) VALUES (@u, @p, @n, @t, @d)"},
                // Возвращает значение
                { Commands.CheckUser, "SELECT pass FROM UserTable WHERE login = @u"},
                { Commands.GetUserNameByLogin, "SELECT name FROM UserTable WHERE login = @u"},
                // { Commands.AddArticle, "INSERT INTO Articles (header, content, creation, author) VALUES (@h, @co, @cr, @a)"},
                // { Commands.EditArticle, "UPDATE Articles SET header = @h, content = @co, creation = @cr WHERE id = @i"},
                // { Commands.DeleteArticle, "DELETE FROM Articles WHERE id = @i"},
                // { Commands.GetArticle, "SELECT header, content, author, creation FROM Articles WHERE id = @i"},
                // { Commands.GetAllArticles, "SELECT id, header, content, author, creation FROM Articles ORDER BY creation DESC"},
                // { Commands.GetAuthorArticles, "SELECT id, header, content, creation FROM Articles WHERE author = @a ORDER BY creation DESC"},
                // { Commands.GetAuthorIDByName, "SELECT id FROM Users WHERE username = @u"},
                // { Commands.GetAuthorNameByID, "SELECT username FROM Users WHERE id = @i"},
                // { Commands.GetArticleAuthorByID, "SELECT author FROM Articles WHERE id = @i"},
                // { Commands.GetAuthorLevelByName, "SELECT level FROM Users WHERE username = @u"},
            };
        }

        public string GetCommand(Commands command) => _commands[command];
    }
}
