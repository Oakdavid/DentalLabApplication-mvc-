using DentalLabConsoleApplicationWithAdo.Models.Entities;
using DentalLabConsoleApplicationWithAdo.Repository.Interface;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Repository.Implementation
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public void Create(Appointment obj)
        {
            int generatedId = -1;
             
            var tinyDeleted = obj.IsDeleted ? 1 : 0;
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                string insertQuery = $"INSERT INTO appointment (RefNo, PatientId , DateOfAppointment, AppointmentStatus, AppointmentType, IsDeleted) " +
                $"VALUES ('{obj.RefNumber}', '{obj.PatientId}', '{obj.DateOfAppointment?.ToString("yyyy-MM-dd / hh-mm-ss")}'," +
                $"'{obj.AppointmentStatus}','{obj.AppointmentType}', '{tinyDeleted}')";

                var command = new MySqlCommand(insertQuery, conn);
                var input = command.ExecuteNonQuery();
                if (input > 0)
                {
                    //Console.WriteLine("Appointment created successfully");
                }
                else
                {
                    Console.WriteLine("No appointment created");
                }
            }

        }

        public Appointment Get(string refNumber)
        {
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                var command = new MySqlCommand($"select * from appointment where RefNo = '{refNumber}'", conn);
                var appointmentReader = command.ExecuteReader();
                while (appointmentReader.Read())
                {
                    return new Appointment
                    {
                        Id = (int)appointmentReader["Id"],
                        RefNumber = appointmentReader["RefNo"].ToString(),
                        DateOfAppointment = DateTime.Parse(appointmentReader["DateOfAppointment"].ToString()),
                        AppointmentStatus = Models.Enum.AppointmentStatus.Initialized,
                        AppointmentType = Models.Enum.AppointmentType.PhysicalAppointment,
                        IsDeleted = false,
                    };
                }
            }
            return null;
        }

        public Appointment GetById(int Id)
        {
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                var command = new MySqlCommand($"select * from appointment where Id = '{Id}'", conn);
                var appointmentReader = command.ExecuteReader();
                while (appointmentReader.Read())
                {
                    return new Appointment
                    {
                        Id = (int)appointmentReader["Id"],
                        RefNumber = appointmentReader["RefNo"].ToString(),
                        DateOfAppointment = DateTime.Parse(appointmentReader["DateOfAppointment"].ToString()),
                        AppointmentStatus = Models.Enum.AppointmentStatus.Initialized,
                        AppointmentType = Models.Enum.AppointmentType.PhysicalAppointment,
                        IsDeleted = false,
                    };
                }
            }
            return null;
        }


        public Appointment GetByPatientNo(int patientNo)
        {
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                var command = new MySqlCommand($"select * from appointment where PatientId = '{patientNo}'", conn);
                var appointmentReader = command.ExecuteReader();
                while (appointmentReader.Read())
                {
                    return new Appointment
                    {
                        Id = (int)appointmentReader["Id"],
                        RefNumber = appointmentReader["RefNo"].ToString(),
                        DateOfAppointment = DateTime.Parse(appointmentReader["DateOfAppointment"].ToString()),
                        AppointmentStatus = Models.Enum.AppointmentStatus.Initialized,
                        AppointmentType = Models.Enum.AppointmentType.PhysicalAppointment,
                        IsDeleted = false,

                    };
                }
            }
            return null;
        }
        public Appointment GetLastId()
        {
            using (MySqlConnection conn = new MySqlConnection(DentalLabDbContext.connections))
            {
                conn.Open();
                var command = new MySqlCommand("SELECT * FROM appointment ORDER BY Id DESC LIMIT 1", conn);
                var appointmentReader = command.ExecuteReader();
                while (appointmentReader.Read())
                {
                    return new Appointment
                    {
                        Id = (int)appointmentReader["Id"],
                        RefNumber = appointmentReader["RefNo"].ToString(),
                        DateOfAppointment = DateTime.Parse(appointmentReader["DateOfAppointment"].ToString()),
                        AppointmentStatus = Models.Enum.AppointmentStatus.Initialized,
                        AppointmentType = Models.Enum.AppointmentType.PhysicalAppointment,
                        IsDeleted = false,
                    };
                }
            }
            return null;
        }

        public List<Appointment> GetAll()
        {
            var list = new List<Appointment>();
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                var command = new MySqlCommand($"select * from Appointment", conn);
                var appointments = command.ExecuteReader();
                while (appointments.Read())
                {
                    list.Add(new Appointment
                    {
                        Id = (int)appointments["Id"],
                        RefNumber = appointments["RefNo"].ToString(),
                        DateOfAppointment = DateTime.Parse(appointments["DateOfAppointment"].ToString()),
                        AppointmentStatus = Models.Enum.AppointmentStatus.Initialized,
                        AppointmentType = Models.Enum.AppointmentType.PhysicalAppointment,
                        IsDeleted =false,
                    });
                }
            }
            return list;
        }
        public List<Appointment> GetAllInitialized()
        {
            var list = new List<Appointment>();
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                var command = new MySqlCommand($"select * from Appointment where AppointmentStatus = 'Initialized'", conn);
                var appointments = command.ExecuteReader();
                while (appointments.Read())
                {
                    list.Add(new Appointment
                    {
                        Id = (int)appointments["Id"],
                        RefNumber = appointments["RefNo"].ToString(),
                        DateOfAppointment = DateTime.Parse(appointments["DateOfAppointment"].ToString()),
                        AppointmentStatus = Models.Enum.AppointmentStatus.Initialized,
                        AppointmentType = Models.Enum.AppointmentType.PhysicalAppointment,
                        IsDeleted =false,
                        PatientId = (int)appointments["PatientId"],
                    });
                }
            }
            return list;
        }

        public List<Appointment> GetAllAssignedAppointment()
        {
            var list = new List<Appointment>();
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                var command = new MySqlCommand($"select * from Appointment where AppointmentStatus = 'Assigned'", conn);
                var appointments = command.ExecuteReader();
                while (appointments.Read())
                {
                    list.Add(new Appointment
                    {
                        Id = (int)appointments["Id"],
                        RefNumber = appointments["RefNo"].ToString(),
                        DateOfAppointment = DateTime.Parse(appointments["DateOfAppointment"].ToString()),
                        AppointmentStatus = Models.Enum.AppointmentStatus.Assigned,
                        AppointmentType = Models.Enum.AppointmentType.PhysicalAppointment,
                        IsDeleted = false,
                        PatientId = (int)appointments["PatientId"],
                    });
                }
            }
            return list;
        }

        public List<Appointment> GetAppointmentByDoctorId(int id)
        {
            var list = new List<Appointment>();
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                var command = new MySqlCommand($"select * from Appointment where DoctorId = '{id}'", conn);
                var appointments = command.ExecuteReader();
                while (appointments.Read())
                {
                    list.Add(new Appointment
                    {
                        Id = (int)appointments["Id"],
                        RefNumber = appointments["RefNo"].ToString(),
                        DateOfAppointment = DateTime.Parse(appointments["DateOfAppointment"].ToString()),
                        AppointmentStatus = Models.Enum.AppointmentStatus.Initialized,
                        AppointmentType = Models.Enum.AppointmentType.PhysicalAppointment,
                        IsDeleted = false,
                        PatientId = (int)appointments["PatientId"],
                        DoctorId = (int)appointments["DoctorId"],
                    });
                }
            }
            return list;
        }

        public bool UpdateAppointmentWithDoctorId(Appointment appointment)
        {
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                string query = $"update appointment set doctorId = '{appointment.DoctorId}' , AppointmentStatus = '{appointment.AppointmentStatus}' where Id = '{appointment.Id}'";

                var command = new MySqlCommand(query, conn);

                var doctorUpdate = command.ExecuteNonQuery();
                if (doctorUpdate > 0)
                {
                    new Appointment
                    {
                        Id = appointment.Id,
                        AppointmentStatus = Models.Enum.AppointmentStatus.Assigned, // just added this
                    };
                    return true;
                }
            }
            return false;
        }
    }
}
