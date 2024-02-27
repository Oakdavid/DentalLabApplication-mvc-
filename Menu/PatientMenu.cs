using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Models.Entities;
using DentalLabConsoleApplicationWithAdo.Models.Enum;
using DentalLabConsoleApplicationWithAdo.Repository.Interface;
using DentalLabConsoleApplicationWithAdo.Service.Implementation;
using DentalLabConsoleApplicationWithAdo.Service.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Menu
{
    public class PatientMenu
    {
        IPatientService _patientService = new PatientService();
        IUserService _userService = new UserService();
        IProfileService _profileService = new ProfileService();
        IAppointmentService _appointmentService = new AppointmentService();
        IDoctorService _doctorService = new DoctorService();
        IReportService _reportService = new ReportService();
        IDentalServiceService _dentalServiceService = new DentalServiceService();
        public void Patient()
        {
            Console.WriteLine("Press 1 to view all DentalLab Services \nPress 2 to Book an Appointment " +
                "\nPress 3 to view appointment\nPress 4 to view doctors report \nPress 0 to Logout"); 
            string options = Console.ReadLine();

            if (options == "1")
            {
                GetAllDentalServices();
                Patient();
            }

            else if (options == "2")
            {
                BookAppointment();
                Patient();
            }

            else if (options == "3")
            {
                ViewAppointment();
                Patient();
            }

            else if (options == "4")
            {
                ViewDoctorsReports();
                Patient();
                Patient();
            }
            else if (options == "0")
            {
                Main mainMenu = new Main();
                mainMenu.MainMenu();
            }

            else
            {
                Console.WriteLine("Invalid input. Please select a valid number from the option");
                Patient();
            }
        }

        public void PatientRegistration()
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
                    Console.WriteLine("Enter your Date of Birth in this format (yyyy-MM-dd):");
                    dateOfBirth = DateTime.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid date format. Please enter the date in yyyy-MM-dd format.");
                }
            }

            Console.WriteLine("Enter 1 for Male or Enter 2 for Female");
            int userInput = int.Parse(Console.ReadLine());
            Gender gender = Gender.Male;

            if (userInput == 1)
            {
                gender = Gender.Male;
            }
            else if(userInput == 2)
            {
                gender = Gender.Female;
            }
            else
            {
                Console.WriteLine("Enter the valid number");
            }
            Console.WriteLine();

            Console.WriteLine("Enter your Email");
            string email = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Enter your Password");
            string password = Console.ReadLine();

            PatientRequestModelDto patientDto = new PatientRequestModelDto()
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                Contact = contact,
                DateOfBirth = dateOfBirth,
                Gender = gender,
                Email = email,
                Password = password,
            };
            _patientService.Create(patientDto);
            Console.WriteLine();
        }                   // done

        public void GetAllDentalServices()                      //1    done
        {
            HeadDoctorMenu headDoctorMenu = new HeadDoctorMenu();
            {
                    
                headDoctorMenu.DentalServices();
            }
        }

        public void ViewDoctorsReports()                           // 4
        {
            var patient = _patientService.GetPatientId(Main.LoggedInId);

            var doctorsReport = _reportService.Get(patient.PatientCardNo);

            if (doctorsReport != null)
            {

                Console.WriteLine($"Reference Number:  {doctorsReport.RefNumber} " +
                    $"Doctors Name: {doctorsReport.DrName} " +
                    $"PatientCardNo: {doctorsReport.PatientCardNo}" +
                    $" Doctors Report: {doctorsReport.ReportContent}");

            }
            else
            {
                Console.WriteLine("Go and see a Doctor, no record found");
                Console.WriteLine();
            }
        }

        public void BookAppointment()                       // 2
        {
            var allServices = _dentalServiceService.GetAllService();
            foreach (var service in allServices)
            {
                Console.WriteLine($"Name: {service.Name}, Code: {service.Code}Description: {service.Description}");
            }
            Console.WriteLine("Select from the code that suit your area of complain to book an appointment");
            Console.WriteLine("Enter 1 for code one\nEnter 2 for code three\nEnter 3 for code three\nEnter 4 for code four");
            string options = Console.ReadLine();

            if (options == "1" || options == "2" || options == "3" || options == "4")  // code need to tally with database
            {
                Console.WriteLine("Enter your complaint");
                string patientComplain = Console.ReadLine();
                var currentPatients = _patientService.GetPatientId(Main.LoggedInId);
                var appointmentExist = _appointmentService.ViewAppointment(currentPatients.Id);
                if (appointmentExist != null)
                {
                    Console.WriteLine("You already have an existing appointment. Please cancel or reschedule it if needed.");
                    return;
                }

                Console.WriteLine("Enter 1 for Physical or Enter 2 for virtual"); // coming to this
                AppointmentRequestModel appointmentRequest = new AppointmentRequestModel()
                {
                    PatientComplain = patientComplain,
                    AppointmentType = Models.Enum.AppointmentType.PhysicalAppointment,
                    CardNo = currentPatients.PatientCardNo,
                    PatientId = currentPatients.Id
                };
                _appointmentService.Create(appointmentRequest);
                Console.WriteLine("Appointment successfully booked!");
            }
            else
            {
                Console.WriteLine("Enter a valid code");
            }


            //Console.WriteLine("Enter your complain");
            //string patientComplain = Console.ReadLine();

            //var currentPatient = _patientService.GetPatientId(Main.LoggedInId);

            //var patientAppointments = _appointmentService.ViewAppointment(currentPatient.PatientCardNo);
            //if (patientAppointments != null )  
            //{
            //    Console.WriteLine("You already have an existing appointment. Please cancel or reschedule it if needed.");
            //    return;
            //}
            //AppointmentRequestModel appointmentRequestModel = new AppointmentRequestModel()
            //{
            //    PatientComplain = patientComplain,
            //    AppointmentType = Models.Enum.AppointmentType.PhysicalAppointment,
            //    CardNo = currentPatient.PatientCardNo,
            //    PatientId = currentPatient.Id
            //};
            //_appointmentService.Create(appointmentRequestModel);
            //Console.WriteLine("Appointment successfully booked!");

            //Console.WriteLine();
        }

        public void ViewAppointment()                           //3
        {
            var patients = _patientService.GetPatientId(Main.LoggedInId);
            var patientAppointment = _appointmentService.ViewAppointment(patients.Id);
            if(patientAppointment != null)
            {
                Console.WriteLine($"The date of Appointment is {patientAppointment.DateOfAppointment}");
            }
        }
    }
}
