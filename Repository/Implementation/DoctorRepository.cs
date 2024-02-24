using DentalLabConsoleApplicationWithAdo.Menu;
using DentalLabConsoleApplicationWithAdo.Models.Entities;
using DentalLabConsoleApplicationWithAdo.Repository.Interface;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Repository.Implementation
{
    public class DoctorRepository : IDoctorRepository
    {
        public void Create(Doctor doctor)
        {

            var tinyDeleted = doctor.IsDeleted ? 1 : 0;
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                string insertQuery = $"INSERT INTO doctor (LicenseNumber,ProfileId, Education, YearsOfExperience, Specializations,SpecializationDescription, IsDeleted) " +
                $"VALUES ('{doctor.LicenseNumber}', '{doctor.ProfileId}','{doctor.Education}', '{doctor.YearsOfExperience}'," +
                $"'{doctor.Specializations}', '{doctor.SpecializationDescription}', '{tinyDeleted}')";

                var command = new MySqlCommand(insertQuery, conn);

                var input = command.ExecuteNonQuery();
                if (input > 0)
                {
                    Console.WriteLine("Doctor created successfully");
                }
                else
                {
                    Console.WriteLine("No doctor created");
                }
            }
        }

        public Doctor Get(string licenseNumber) 
        {
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                string query = $"select * from doctor where LicenseNumber = '{licenseNumber}'";
                var command = new MySqlCommand (query, conn);
                var doctorReader = command.ExecuteReader();
                while (doctorReader.Read())
                {
                    return new Doctor
                    {
                        Id = (int)doctorReader["Id"],
                        LicenseNumber = doctorReader["LicenseNumber"].ToString(),
                        Education = doctorReader["Education"].ToString(),
                        YearsOfExperience = (int)doctorReader["YearsOfExperience"],
                        Specializations = doctorReader["Specializations"].ToString(),
                        SpecializationDescription = doctorReader["SpecializationDescription"].ToString(),
                        ProfileId = (int)doctorReader["ProfileId"],
                        IsDeleted = Convert.ToBoolean(doctorReader["IsDeleted"]),
                    };

                }
            }
            return null;
        }

        public Doctor GetByEmail(string email) 
        {
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                string query = $"select * from doctor where Email = '{email}'";
                var command = new MySqlCommand(query, conn);
                var doctorReader = command.ExecuteReader();
                while (doctorReader.Read())
                {
                    return new Doctor
                    {
                        Id = (int)doctorReader["Id"],
                        LicenseNumber = doctorReader["LicenseNumber"].ToString(),
                        Education = doctorReader["Education"].ToString(),
                        YearsOfExperience = (int)doctorReader["YearsOfExperience"],
                        Specializations = doctorReader["Specializations"].ToString(),
                        SpecializationDescription = doctorReader["SpecializationDescription"].ToString(),
                        ProfileId = (int)doctorReader["ProfileId"],
                        IsDeleted = Convert.ToBoolean(doctorReader["IsDeleted"]),
                    };

                }
            }
            return null;
        }

        public List<Doctor> GetAll()
        {
           var doctorList = new List<Doctor>();
           using(MySqlConnection conn = new(DentalLabDbContext.connections))
           {
                conn.Open();
                string query = $"select * from doctor";
                var command = new MySqlCommand(query, conn);
                var doctorReader = command.ExecuteReader();
                while (doctorReader.Read())
                {
                    doctorList.Add(new Doctor
                    {
                        Id = (int)doctorReader["Id"],
                        LicenseNumber = doctorReader["LicenseNumber"].ToString(),
                        Education = doctorReader["Education"].ToString(),
                        YearsOfExperience = (int)doctorReader["YearsOfExperience"],
                        Specializations = doctorReader["Specializations"].ToString(),
                        SpecializationDescription = doctorReader["SpecializationDescription"].ToString(),
                        ProfileId = (int)doctorReader["ProfileId"],
                        IsDeleted = Convert.ToBoolean(doctorReader["IsDeleted"])

                    });
                }
                return doctorList;
           }
        }

        public Doctor GetById(int id)
        {
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                string query = $"select * from doctor where Id = '{id}'";
                var command = new MySqlCommand(query, conn);
                var doctorReader = command.ExecuteReader();
                while (doctorReader.Read())
                {
                    return new Doctor
                    {
                        Id = (int)doctorReader["Id"],
                        LicenseNumber = doctorReader["LicenseNumber"].ToString(),
                        Education = doctorReader["Education"].ToString(),
                        YearsOfExperience = (int)doctorReader["YearsOfExperience"],
                        Specializations = doctorReader["Specializations"].ToString(),
                        SpecializationDescription = doctorReader["SpecializationDescription"].ToString(),
                        ProfileId = (int)doctorReader["ProfileId"],
                        IsDeleted = Convert.ToBoolean(doctorReader["IsDeleted"]),
                    };

                }
            }
            return null;
        }

        public bool Update(Doctor doctor)
        {
            var tinyIsDeleted = doctor.IsDeleted ? 1 : 0;
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                string query = $"update doctor set LicenseNumber = '{doctor.LicenseNumber}', Education = '{doctor.Education}', YearsOfExperience = '{doctor.YearsOfExperience}', Specializations = '{doctor.Specializations}'," +
                    $" SpecializationDescription = '{doctor.SpecializationDescription}', IsDeleted = '{tinyIsDeleted}' where Id = '{doctor.Id}'";

                var command = new MySqlCommand (query, conn);

                var doctorUpdate = command.ExecuteNonQuery();
                if (doctorUpdate > 0)
                {
                    new Doctor
                    {
                        Id = doctor.Id,
                        LicenseNumber = doctor.LicenseNumber,
                        Education= doctor.Education,
                        YearsOfExperience = doctor.YearsOfExperience,
                        Specializations = doctor.Specializations,
                        SpecializationDescription = doctor.SpecializationDescription,
                        ProfileId = (int)doctor.ProfileId,
                        IsDeleted = Convert.ToBoolean(tinyIsDeleted),
                    };
                    return true;
                }
            }
            return false;
        }
    }
}
