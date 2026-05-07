using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class Task_service
    {
        public static User Read_User(SQLiteDataReader reader, int offset)
        {
            return new User
            {
                Id = reader.GetInt32(offset),
                Login = reader.GetString(offset + 1),
                Password = reader.GetString(offset + 2),
                Name = reader.GetString(offset + 3),
                Role = (UserRole)reader.GetInt32(offset + 4)
            };
        }

        public List<TaskItem> Get_Tasks(User currentUser, string category = "все")
        {
            var resultat = new List<TaskItem>();
            using (var connection = DbContext.CreateConnection())
            using (var command = connection.CreateCommand())
            {
            }
            return resultat;
        }
        public List<User> Get_Workes()
        {
            var result = new List<User>();
            using (var connection = DbContext.CreateConnection())
            using (var command = new SQLiteCommand("SELECT Id, Login, Password, Name, Role FROM Users WHERE Role = 2 ORDER BY Name;", connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read()) result.Add(Read_User(reader, 0));
            }
            return result;
        }

        public void Delete_Task(TaskItem task, User currentUser)
        {

        }
        public void Add_Task(TaskItem task, User currentUser)
        {

        }
        public List<string> Get_Categori(User currentUser)
        {
            var categories = new List<string>();
            return categories; 
            //потом короче
        }

        public bool Edit_Task(TaskItem task, User currentUser)
        {
            return currentUser.Role == UserRole.Manager || task.CreatedByUserId == currentUser.Id;
        }
    }
}
