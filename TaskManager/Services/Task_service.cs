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

        private static TaskItem Read_Task(SQLiteDataReader reader)
        {
            var task = new TaskItem
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Title = reader.GetString(2),
                Category = reader.GetString(3),
                IsCompleted = reader.GetInt32(4) == 1,
                CreatedByUserId = reader.GetInt32(5),
                AssignedToUserId = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                IsForAllWorkers = reader.GetInt32(7) == 1,
                CreatedByUser = Read_User(reader, 8)
            };

            if (!reader.IsDBNull(13)) task.AssignedToUser = Read_User(reader, 13);
            return task;
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
        private TaskItem Task_Id(int id)
        {
            using (var connection = DbContext.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
SELECT t.Id, t.Name, t.Title, t.Category, t.IsCompleted,
       t.CreatedByUserId, t.AssignedToUserId, t.IsForAllWorkers,
       cu.Id, cu.Login, cu.Password, cu.Name, cu.Role,
       au.Id, au.Login, au.Password, au.Name, au.Role
FROM TaskItems t
JOIN Users cu ON cu.Id = t.CreatedByUserId
LEFT JOIN Users au ON au.Id = t.AssignedToUserId
WHERE t.Id = @Id;";
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    return reader.Read() ? Read_Task(reader) : null;
                }
            }
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
            var tasks = Task_Id(task.Id);
            if (tasks == null) return;
        }
        public void Add_Task(TaskItem task, User currentUser)
        {
            var tasks = Task_Id(task.Id);
            if (tasks == null) return;
        }
        public List<string> Get_Categori(User currentUser)
        {
            var categories = Get_Tasks(currentUser)
                .Select(t => t.Category)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct()
                .OrderBy(c => c)
                .ToList();
            categories.Insert(0, "Все");
            return categories;
        }

        public bool Edit_Task(TaskItem task, User currentUser)
        {
            return currentUser.Role == UserRole.Manager || task.CreatedByUserId == currentUser.Id;
        }
    }
}
