using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Repository.Interface;
using DentalLabConsoleApplicationWithAdo.Service.Implementation;
using DentalLabConsoleApplicationWithAdo.Service.Interface;
using System;
using System.Collections.Generic;
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
        public void Patient()
        {
            Console.WriteLine("Press 1 to view all doctor service \nPress 2 to book an Appointment " +
                "\nPress 3 to view doctors report\nPress 4 to view appointment \nPress 0 to Logout");
            string options = Console.ReadLine();

            if (options == "1")
            {
                GetAllDoctorServices();
                Patient();
            }

            else if (options == "2")
            {
                BookAppointment();
                Patient();
            }

            else if (options == "3")
            {
                ViewDoctorsReports();       
                Patient();
            }

            else if (options == "4")
            {
                Console.WriteLine("work in progress");
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

            Console.WriteLine("Enter your Gender");
            string gender = Console.ReadLine();
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
        }

        public void GetAllDoctorServices()                      //1
        {
           var doctorService = _doctorService.GetAllService();
            foreach(var doctor in doctorService)
            {
                _doctorService.ToString(doctor);
                Console.WriteLine();
            }
        }

        public void ViewDoctorsReports()                           // 3
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
            Console.WriteLine("Enter your complain");
            string patientComplain = Console.ReadLine();

            AppointmentRequestModel appointmentRequestModel = new AppointmentRequestModel()
            {
                PatientComplain = patientComplain,
                AppointmentType = Models.Enum.AppointmentType.PhysicalAppointment,
                DateOfAppointment = DateTime.Now,
            };
            _appointmentService.Create(appointmentRequestModel);
            Console.WriteLine();
        }

        public void ViewAppointment()
        {

        }
    }
}
