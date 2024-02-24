using DentalLabConsoleApplicationWithAdo.Models.Entities;
using DentalLabConsoleApplicationWithAdo.Repository.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        public void Create(User user)
        {
            var tinyDeleted = user.IsDeleted ? 1 : 0;
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                string insertQuery = $"INSERT INTO user (Email, Password, Role, IsDeleted) " +
                $"VALUES ('{user.Email}', '{user.Password}', '{user.Role}','{tinyDeleted}')";  // check here

                var command = new MySqlCommand(insertQuery, conn);

                var input = command.ExecuteNonQuery();
                if (input > 0)
                {
                    //Console.WriteLine("User created successfully");
                }
              //  Console.WriteLine("Not affected");
            }
        }

        public User Get(string email)
        {
            using(MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                string insertQuery = $"select * from user where Email = '{email}'";
                var command = new MySqlCommand(insertQuery, conn);
                var userReader = command.ExecuteReader();
                while (userReader.Read())
                {
                    return new User
                    {
                        Id = (int)userReader["Id"],
                        Email = userReader["Email"].ToString(),
                        Password = userReader["Password"].ToString(),
                        Role = userReader["Role"].ToString(),
                        IsDeleted = Convert.ToBoolean(userReader["Isdeleted"])
                    };
                }
            }
            return null;
        }
        public User Get(int id)
        {
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                string insertQuery = $"select * from user where Id = '{id}'";
                var command = new MySqlCommand(insertQuery, conn);
                var userReader = command.ExecuteReader();
                while (userReader.Read())
                {
                    return new User
                    {
                        Id = (int)userReader["Id"],
                        Email = userReader["Email"].ToString(),
                        Password = userReader["Password"].ToString(),
                        Role = userReader["Role"].ToString(),
                        IsDeleted = Convert.ToBoolean(userReader["Isdeleted"])
                    };
                }
            }
            return null;
        }

        public List<User> GetAll()
        {
            var list = new List<User>();
            using(MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                string insertQuery = $"select * from User";
                var command = new MySqlCommand(insertQuery, conn);
                var userReader = command.ExecuteReader();
                while(userReader.Read())
                {
                    list.Add(new User
                    {
                        Id = (int)userReader["Id"],
                        Email = userReader["Email"].ToString(),
                        Password = userReader["Password"].ToString(),
                        Role = userReader["Role"].ToString(),
                        IsDeleted = Convert.ToBoolean(userReader["Isdeleted"])

                    });
                }
            }
            return list;
        }

        public User LoginByEmailAndPassword(string email, string password)
        {
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                var query = $"select * from user where Email = '{email}' and Password = '{password}'";
                var command = new MySqlCommand (query, conn);
                var userReader = command.ExecuteReader();
                while(userReader.Read())
                {
                    return new User
                    {
                        Email = userReader["Email"].ToString(),
                        Password = userReader["Password"].ToString(),
                        Role = userReader["Role"].ToString(),
                        Id = (int)userReader["Id"]
                        
                    };
                }
            }
            return null;
        }

        public int GetById()
        {
            using (var conn = new MySqlConnection(DentalLabDbContext.connections))
            {
                conn.Open();
                var query = "Select max(id) from User";
                var command = new MySqlCommand(query, conn);
                var userId = (int)(command.ExecuteScalar());
                return userId;
            }
        }
    }
}
