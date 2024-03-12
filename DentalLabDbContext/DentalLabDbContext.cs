using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo
{
    public class DentalLabDbContext
    {
        string connection = "Server = localhost; user = root; password = David1234567890";
       public static string connections = "Server = localhost; user = root; password = David1234567890; database = DentalLabDb";

        string createAppointment = "create table if not exists Appointment(Id int auto_increment primary key, RefNo varchar(50), PatientId int,foreign Key (PatientId) references Patient(Id), DoctorId int, foreign Key (DoctorId) references Doctor(Id), DateOfAppointment datetime, AppointmentType Enum('VirtualAppointment', 'PhysicalAppointment'), AppointmentStatus Enum('Initialized', 'Assigned'),IsDeleted tinyint)";

        string createPatient = "create table if not exists Patient(Id int auto_increment primary key, ProfileId int, CardNo varchar(50), DrLicenseNumber varchar(50), IsDeleted tinyint,foreign KEY (ProfileId) references Profile(Id))";

        string createDentalService = "create table if not exists DentalService(Id int auto_increment primary key, Name varchar(50), Description varchar(225), Code varchar(50), Cost Decimal(10, 2),IsDeleted tinyint)";

        string createProfile = "create table if not exists Profile(Id int auto_increment primary key, UserId int, FirstName varchar(30), LastName varchar(50), Address varchar(50), Contact varchar(50), DateOfBirth datetime, Gender Enum('Male', 'Female'), IsDeleted tinyint, foreign KEY (UserId) references user(Id))";


        string createReport = "create table if not exists Report(Id int auto_increment primary key, AppointmentId int,foreign Key (AppointmentId) references Appointment(Id), ReportContent varchar (225), PatientComplain varchar(225), IsDeleted tinyint)";
          

        string createHeadDoctor = "create table if not exists HeadDoctor (Email varchar(50), Password varchar (50))";

        string createDoctor = "create table if not exists Doctor(Id int auto_increment primary key,ProfileId int, foreign Key (ProfileId) references Profile(Id), LicenseNumber varchar(50)," +
                              " Education varchar(50), YearsOfExperience int, Specializations varchar(50), SpecializationDescription varchar(200), IsAvailable tinyint, IsDeleted tinyint)";


        string createUser = "create table if not exists user(Id int auto_increment primary key, Email varchar(50), Password varchar(50), Role varchar(50), IsDeleted tinyint)";






        public void Tables()
        {

            using (var conn = new MySqlConnection(connection))
            {
                conn.Open();
                var schemaQuerry = "create schema if not exists DentalLabDb";
                var tablesCreated = new MySqlCommand(schemaQuerry, conn);
                tablesCreated.ExecuteNonQuery();
            }
            createTables(createUser);
            createTables(createProfile);
            createTables(createPatient);
            createTables(createDoctor);
            createTables(createDentalService);
            createTables(createAppointment);
            createTables(createReport);
            createTables(createHeadDoctor);

            Console.WriteLine("Tables created successfully");
        }

        private void createTables(string querry)
        {
            using (var conn = new MySqlConnection(connections))
            {
                conn.Open();
                var connect = new MySqlCommand(querry, conn);
                connect.ExecuteNonQuery();

            }
        }

    }
}
