using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Models.Entities;
using DentalLabConsoleApplicationWithAdo.Service.Implementation;
using DentalLabConsoleApplicationWithAdo.Service.Interface;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Menu
{
    public class DoctorMenu()
    {
        IDoctorService _doctorService = new DoctorService();
        IPatientService _patientService = new PatientService();
        IProfileService _profileService = new ProfileService(); 
        IReportService _reportService = new ReportService();
        IAppointmentService _appointmentService = new AppointmentService();

        public void Doctor()
        {
            Console.WriteLine("Enter 1 to view to assigned Patient\nEnter 2 to view  assiged appointment " +
                "\nEnter 3 to send report \nPress 4 to update specialization\nEnter 0 to go back to Main Menu");
            string options = Console.ReadLine();

            if (options == "1")
            {
                ViewAllPatient();
                Doctor();
                Console.WriteLine();

            }

            else if (options == "2")
            {
                ViewAllAppointment();
                Doctor();
            }

            else if (options == "3")
            {
                Report();
                Doctor();
            }

            else if(options == "4")
            {
                UpdateSpecializations();
                Doctor();
            }

            else if (options == "0")
            {
                Main main = new Main();
                main.MainMenu();
            }
            else
            {
                Console.WriteLine("Please enter a valid number");
            }
        }

        public void DoctorRegistration()
        {
           
            Console.WriteLine("Enter your First Name");
            string firstName = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Enter your last Name");
            string lastName = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Enter your Address");
            string address = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Enter your Contact");
            string contact = Console.ReadLine();
            Console.WriteLine();
            DateTime dateOfBirth;
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter your Date of Birth in this format (yyyy-MM-dd 00:00:00):");
                    dateOfBirth = DateTime.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid date format. Please enter the date in yyyy-MM-dd format.");
                }
            }


            Console.WriteLine();
            Console.WriteLine("Enter your Gender");
            string gender = Console.ReadLine();

            

            Console.WriteLine("Enter your email");
            string email = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Enter your Password");
            string password = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Enter your licenseNumber");
            string licenseNumber = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Enter your Education");
            string education = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Enter Years of Experience");
            int yearOfExperience = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Specialization");
            string specialization = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Specialization Description");
            string specializationDescription = Console.ReadLine();

            DoctorRequestRegistrationDto doctorRequestRegistrationDto = new DoctorRequestRegistrationDto()
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                Contact = contact,
                DateOfBirth = dateOfBirth,
                Gender = Models.Enum.Gender.Male,
                SpecializationDescription = specializationDescription,
                
                
                Email = email,
                Password = password,
                LicenseNumber = licenseNumber,
                Education = education,
                YearsOfExperience = yearOfExperience,
                Specializations = specialization
            };
            _doctorService.Create(doctorRequestRegistrationDto);
        }
        public void ViewAllPatient()
        {
            var allPatient = _patientService.GetAll();
            if(allPatient == null)
            {
                Console.WriteLine("No Patient available at this time");
               
            }
            foreach (var patient in allPatient)
            {
                _patientService.ToString(patient);
            }
        }

        public void ViewAllAppointment()
        {
            var allAppointment = _appointmentService.GetAll();
            foreach (var appointment in allAppointment)
            {
                _appointmentService.ToString(appointment);
            }
        }

        public void Report()
        {
            Console.WriteLine("Enter report context");
            string reportContent = Console.ReadLine();

            ReportRequestModelDto reportRequestModelDto = new ReportRequestModelDto()
            {
            
                ReportContent = reportContent,
                //public string RefNumber { get; set; }
                //public string DrName { get; set; }
                //public string PatientCardNo { get; set; }
            };
            _reportService.Create(reportRequestModelDto);       // which user am i giving a report too
            Console.WriteLine();
        }

        public void UpdateSpecializations()
        {
            Console.WriteLine("Enter your licenseNUmber");
            string licenseNUmber = Console.ReadLine();

            Console.WriteLine("Enter your Education");
            string education = Console.ReadLine();

            Console.WriteLine("Enter your Years of Specialization");
            int yearOfSpecialization = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter your Specialization");
            string specialization = Console.ReadLine();

            Console.WriteLine("Enter your SpecializationDescription");
            string specializationDescription = Console.ReadLine();

            var updates = _doctorService.Get(licenseNUmber);
            if(updates != null)
            {
                UpdateDoctorRequstRegistrationDto update = new UpdateDoctorRequstRegistrationDto()
                {
                    LicenseNumber = licenseNUmber,
                    Education = education,
                    YearsOfExperience = yearOfSpecialization,
                    Specializations = specialization,
                    SpecializationDescription = specializationDescription
                };
                _doctorService.Update(update);
                Console.WriteLine("Doctor Specialization updated successfully");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Doctor not found. Please provide the necessary details");
            }
        }

      
    }
}
