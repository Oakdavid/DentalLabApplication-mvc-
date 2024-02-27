using DentalLabConsoleApplicationWithAdo.Models.Entities;
using DentalLabConsoleApplicationWithAdo.Repository.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace DentalLabConsoleApplicationWithAdo.Repository.Implementation
{
    internal class ProfileRepository : IProfileRepository
    {

        public void Create(Profile profile)
        {
            var tinyDeleted = profile.IsDeleted ? 1 : 0;
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open(); 
                string insertQuery = $"INSERT INTO Profile (UserId, FirstName, LastName, Address, Contact, DateOfBirth, Gender, IsDeleted) " +
                $"VALUES ('{profile.UserId}','{profile.FirstName}', '{profile.LastName}', '{profile.Address}', '{profile.Contact}'," +
                $" '{profile.DateOfBirth.ToString("yyyy-MM-dd / HH:mm:ss")}', '{profile.Gender}', '{tinyDeleted}')";

                var command = new MySqlCommand(insertQuery, conn);

                var input = command.ExecuteNonQuery();
                if (input > 0)
                {
                   // Console.WriteLine("Profile created successfully");
                }
                else
                {
                    Console.WriteLine("No profile created");
                }
            }
        }

        public Profile Get(int id)
        {
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                var query = $"select * from profile where Id = '{id}'";
                var command = new MySqlCommand(query, conn);
                var profileReader = command.ExecuteReader();
                while (profileReader.Read())
                {
                    return new Profile()
                    {
                        Id = (int)profileReader["Id"],
                        FirstName = profileReader["FirstName"].ToString(),
                        LastName = profileReader["LastName"].ToString(),
                        Address = profileReader["Address"].ToString(),
                        Contact = profileReader["Contact"].ToString(),
                        DateOfBirth = DateTime.Parse(profileReader["DateOfBirth"].ToString()),
                        Gender = Models.Enum.Gender.Male,
                        UserId = (int)profileReader["Id"],
                        IsDeleted = Convert.ToBoolean(profileReader["IsDeleted"])
                    };
                }
            }
            return null;
        }

        public Profile Get(string email)
        {
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                var query = $"select * from profile where Email = '{email}'";
                var command = new MySqlCommand(query, conn);
                var profileReader = command.ExecuteReader();
                while (profileReader.Read())
                {
                    return new Profile()
                    {
                        Id = (int)profileReader["Id"],
                        UserId = (int)profileReader["Id"],
                        FirstName = profileReader["FirstName"].ToString(),
                        LastName = profileReader["LastName"].ToString(),
                        Address = profileReader["Address"].ToString(),
                        Contact = profileReader["Contact"].ToString(),
                        DateOfBirth = DateTime.Parse(profileReader["DateOfBirth"].ToString()),
                        Gender = Models.Enum.Gender.Male,
                        IsDeleted = Convert.ToBoolean(profileReader["IsDeleted"])
                    };
                }
            }
            return null;
        }

        public Profile GetProfileByUserId(int userId)
        {
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                var query = $"select * from profile where UserId = '{userId}'";
                var command = new MySqlCommand(query, conn);
                var profileReader = command.ExecuteReader();
                while (profileReader.Read())
                {
                    return new Profile()
                    {
                        Id = (int)profileReader["Id"],
                        UserId = (int)profileReader["Id"],
                        FirstName = profileReader["FirstName"].ToString(),
                        LastName = profileReader["LastName"].ToString(),
                        Address = profileReader["Address"].ToString(),
                        Contact = profileReader["Contact"].ToString(),
                        DateOfBirth = DateTime.Parse(profileReader["DateOfBirth"].ToString()),
                        Gender = Models.Enum.Gender.Male,
                        IsDeleted = Convert.ToBoolean(profileReader["IsDeleted"])
                    };
                }
            }
            return null;
        }

        public List<Profile> GetAll()
        {
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                var profileList = new List<Profile>();
                var query = $"select * from profile";
                var command = new MySqlCommand(query, conn);
                var profileReader = command.ExecuteReader();
                while (profileReader.Read())
                {
                    profileList.Add(new Profile
                    {
                        Id = (int)profileReader["Id"],
                        UserId = (int)profileReader["Id"],
                        FirstName = profileReader["FirstName"].ToString(),
                        LastName = profileReader["LastName"].ToString(),
                        Address = profileReader["Address"].ToString(),
                        Contact = profileReader["Contact"].ToString(),
                        DateOfBirth = DateTime.Parse(profileReader["DateOfBirth"].ToString()),
                        Gender = Models.Enum.Gender.Male,
                        IsDeleted = Convert.ToBoolean(profileReader["IsDeleted"])
                    });
                }
                return profileList;
            }
        }

        public int GetProfileId()
        {
            using (MySqlConnection conn = new MySqlConnection(DentalLabDbContext.connections))
            {
                conn.Open();
                var query = "Select max(id) from profile";
                var command = new MySqlCommand(query, conn);
                var profileId = (int)(command.ExecuteScalar());
                return profileId;
            }
        }
    }
}
