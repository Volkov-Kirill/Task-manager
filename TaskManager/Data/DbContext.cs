using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace TaskManager.Data
{
    public class DbContext
    {
        private const string DatabaseFileName = "taskmanager.db";
        private static bool _initialized;

        public static SQLiteConnection CreateConnection()
        {
            var connection = new SQLiteConnection($"Data Source={DatabaseFileName};");
            connection.Open();
            return connection;
        }


    }
}
