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
            var refNumber = obj.RefNumber + $"RDT/DENTAL/00/{new Random().Next(001, 100)}";
            var cardNumber = obj.CardNo + $"RDT/CARDNO/00/{new Random().Next(50, 100)}";
            var drNumber = $"RDT/DOCTOR/{new Random().Next(3, 5)}";    
            var tinyDeleted = obj.IsDeleted ? 1 : 0;
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                string insertQuery = $"INSERT INTO appointment (RefNo, CardNo, DrNumber, PatientComplain, DateOfAppointment, AppointmentStatus, AppointmentType, IsDeleted) " +
                $"VALUES ('{refNumber}', '{cardNumber}', '{drNumber}', '{obj.PatientComplain}', '{obj.DateOfAppointment.ToString("yyyy-MM-dd / hh-mm-ss")}'," +
                $"'{obj.AppointmentStatus}','{obj.AppointmentType}', '{tinyDeleted}')";

                var command = new MySqlCommand(insertQuery, conn);

                var input = command.ExecuteNonQuery();
                if (input > 0)
                {
                    Console.WriteLine("Appointment created successfully");
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
                        CardNo = appointmentReader["CardNo"].ToString(),
                        DrNumber = appointmentReader["DrNumber"].ToString(),
                        PatientComplain = appointmentReader["PatientComplain"].ToString(),
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
                        CardNo = appointments["CardNo"].ToString(),
                        DrNumber = appointments["DrNumber"].ToString(),
                        PatientComplain = appointments["PatientComplain"].ToString(),
                        DateOfAppointment = DateTime.Parse(appointments["DateOfAppointment"].ToString()),
                        AppointmentStatus = Models.Enum.AppointmentStatus.Initialized,
                        AppointmentType = Models.Enum.AppointmentType.PhysicalAppointment,
                        IsDeleted =false,
                        // ReportContent = appointments["ReportContent"].ToString(),
                    });
                }
            }
            return list;
        }
    }
}
