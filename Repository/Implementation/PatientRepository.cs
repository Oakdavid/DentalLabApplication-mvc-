using DentalLabConsoleApplicationWithAdo.Menu;
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
    internal class PatientRepository : IPatientRepository
    {
        public void Create(Patient obj)
        {
            {
                var cardNumber = obj. CardNo + $"RDT/CARDNO/00/{new Random().Next(50, 100)}";
                var tinyDeleted = obj.IsDeleted ? 1 : 0;
                using (MySqlConnection conn = new(DentalLabDbContext.connections))
                {
                    conn.Open();
                    string insertQuery = $"INSERT INTO patient (CardNo,ProfileId, IsDeleted) " +
                    $"VALUES ('{cardNumber}','{obj.ProfileId}','{tinyDeleted}')";

                    var command = new MySqlCommand(insertQuery, conn);

                    var input = command.ExecuteNonQuery();
                    if (input > 0)
                    {
                        Console.WriteLine("Patient created successfully");
                    }
                    else
                    {
                        Console.WriteLine("No patient created");
                    }
                }
            }
        }

        public Patient GetByCardNo(string cardNo)
        {
            using(MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                var query = $"select * from patient where CardNo = '{cardNo}'";
                var command = new MySqlCommand(query, conn);
                var patientReader = command.ExecuteReader();
                while(patientReader.Read())
                {
                    return new Patient()
                    {
                        Id = (int)patientReader["Id"],
                        CardNo = patientReader["CardNo"].ToString(),
                        ProfileId = (int)patientReader["ProfileId"],
                        IsDeleted = false,
                        
                    };
                }
            }
            return null;
        }

        public List<Patient> GetAll()
        {
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                var patientList = new List<Patient>();
                var query = $"select * from patient";
                var command = new MySqlCommand(query, conn);
                var patientReader = command.ExecuteReader();
                while (patientReader.Read())
                { 
                    patientList.Add(new Patient
                    {
                        Id = (int)patientReader["Id"],
                        CardNo = patientReader["CardNo"].ToString(),
                        ProfileId = (int)patientReader["ProfileId"],
                        IsDeleted = Convert.ToBoolean(patientReader["IsDeleted"])
                    });
                }
                return patientList;
            }
        }

        public Patient GetById(int id)  
        {
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                var query = $"select * from patient where Id = '{id}'";
                var command = new MySqlCommand(query, conn);
                var patientReader = command.ExecuteReader();
                while (patientReader.Read())
                {
                    return new Patient
                    {
                        Id = (int)patientReader["Id"],
                        CardNo = patientReader["CardNo"].ToString(),
                        ProfileId = (int)patientReader["ProfileId"],
                        IsDeleted = Convert.ToBoolean(patientReader["IsDeleted"])
                    };
                }
            }
            return null;
        }

        public Patient GetPatientByProfileId(int id)
        {
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                var query = $"select * from patient where ProfileId = '{id}'";
                var command = new MySqlCommand(query, conn); 
                var patientReader = command.ExecuteReader();
                while (patientReader.Read())
                {
                    return new Patient
                    {
                        Id = (int)patientReader["Id"],
                        CardNo = patientReader["CardNo"].ToString(),
                        ProfileId = (int)patientReader["ProfileId"],
                        IsDeleted = Convert.ToBoolean(patientReader["IsDeleted"])
                    };
                }
            }
            return null;
        }
    }
}
