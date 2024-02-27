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
    internal class DentalServiceRepository : IDentalServiceRepository
    {
        public DentalService Create(DentalService dentalService)
        {
            var tinyDeleted = dentalService.IsDeleted ? 1 : 0;
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                string insertQuery = $"INSERT INTO DentalService (Name,  Description, Code, Cost, IsDeleted) " +
                $"VALUES ('{dentalService.Name}', '{dentalService.Description}','{dentalService.Code}','{dentalService.Cost}', '{tinyDeleted}')";

                var command = new MySqlCommand(insertQuery, conn);
                var input = command.ExecuteNonQuery();
                if (input > 0)
                {
                    Console.WriteLine("Dental Service created successfully");
                    return dentalService;
                }
                else
                {
                    Console.WriteLine("Dental Service not created");
                }
                return null;
            }
        }

        public DentalService Get(int id)
        {
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                var command = new MySqlCommand($"select * from DentalService where Id = '{id}'", conn);
                var serviceReader = command.ExecuteReader();
                while (serviceReader.Read())
                {
                    return new DentalService
                    {
                        Id = (int)serviceReader["Id"],
                        Name = serviceReader["Name"].ToString(),
                        Description = serviceReader["Name"].ToString(),
                        Code = serviceReader["Code"].ToString(),
                        Cost = serviceReader.GetDecimal(serviceReader.GetOrdinal("Cost")),
                        IsDeleted = false,
                    };
                }
            }
            return null;
        }

        public IEnumerable<DentalService> GetAllService()
        {
            var list = new List<DentalService>();
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                var command = new MySqlCommand($"select * from DentalService", conn);
                var serviceReader = command.ExecuteReader();
                while (serviceReader.Read())
                {
                    list.Add(new DentalService
                    {
                        Id = (int)serviceReader["Id"],
                        Name = serviceReader["Name"].ToString(),
                        Description = serviceReader["Name"].ToString(),
                        Code = serviceReader["Code"].ToString(),
                        Cost = serviceReader.GetDecimal(serviceReader.GetOrdinal("Cost")),
                        IsDeleted = false,
                    });
                }
            }
            return list;
        }

        public DentalService Update(DentalService dentalService)
        {
            var tinyIsDeleted = dentalService.IsDeleted ? 1 : 0;
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                string query = $"update DentalService set Id = '{dentalService.Id}', Name = '{dentalService.Name}', Description = '{dentalService.Description}', Code = '{dentalService.Code}'," +
                    $" Cost = '{dentalService.Cost}', IsDeleted = '{tinyIsDeleted}' where Id = '{dentalService.Id}'";

                var command = new MySqlCommand(query, conn);

                var dentalServiceUpdate = command.ExecuteNonQuery();
                if (dentalServiceUpdate > 0)
                {
                    new DentalService
                    {
                        Id = dentalService.Id,
                        Name = dentalService.Name,
                        Description = dentalService.Description,
                        Code = dentalService.Code,
                        Cost = dentalService.Cost,
                        IsDeleted = false,
                    };
                    return dentalService;
                }
                return null;
            }
        }
    }
}
