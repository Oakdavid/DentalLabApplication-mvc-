using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Models.Entities;
using DentalLabConsoleApplicationWithAdo.Service.Implementation;
using DentalLabConsoleApplicationWithAdo.Service.Interface;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Menu
{
    public class HeadDoctorMenu()
    {
        IDoctorService _doctorService = new DoctorService();
        IPatientService _patientService = new PatientService();
        IAppointmentService _appointmentService = new AppointmentService();
        IReportService _reportService = new ReportService();
        IDentalServiceService _dentalServiceService = new DentalServiceService();
       

        public void HeadDoctor()
        {
            Console.WriteLine("Enter 1 to view all Patients\nEnter 2 to view all available Doctors\nEnter 3 to terminate a Doctor\n Enter 4 to assign Doctor \nEnter 5 to Update Dental Service\nEnter 0 to log out");
            string options = Console.ReadLine();
            if (options == "1")
            {
                ViewAllPatient();
                HeadDoctor();
            }
            else if(options == "2")
            {
                AllAvailableDoctors();
                HeadDoctor();
            }
            else if (options == "3")
            {
                Console.WriteLine("work in progress");
            }
            else if (options == "4")
            {

            }
            else if (options == "5")
            {
                UpdateDentalService();
                HeadDoctor();
            }
            else if (options == "0")
            {
                Main mainMenu = new Main();
                mainMenu.MainMenu();
            }
            else
            {
                Console.WriteLine("");
            }
        }

        public void ViewAllPatient()
        {
            var allPatient = _patientService.GetAll();
            if (allPatient == null)
            {
                Console.WriteLine("No Patient available at this time");

            }
            foreach (var patient in allPatient)
            {
                _patientService.ToString(patient);
            }
        }

        public void AllAvailableDoctors()
        {
            var availableDoctor = _doctorService.IsAvailable();
            if(availableDoctor == null)
            {
                Console.WriteLine("No available doctors");
                return;
            }
            foreach (var doctor in availableDoctor)
            {
                Console.WriteLine($"License: {doctor.LicenseNumber}, {doctor.Education}, {doctor.Education}, {doctor.YearsOfExperience}, " +
                    $"{doctor.Specializations}, {doctor.SpecializationDescription}");
            }

        }       //DONE

        public void DentalServices()             // patient can now view all  DONE
        {
            Console.WriteLine("What is the name");
            string name = Console.ReadLine();
            Console.WriteLine("What is the Description");
            string description = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("What is the code");
            string code = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("What is the cost");
            decimal cost = decimal.Parse(Console.ReadLine());

            DentalService dentalService = new DentalService()
            {
                Name = name,
                Description = description,
                Code = code,
                Cost = cost
            };
            _dentalServiceService.Create(dentalService);
        }

        public void UpdateDentalService()           // DONE
        {
            Console.WriteLine("What is the name");
            string name = Console.ReadLine();
            Console.WriteLine("What is the Description");
            string description = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("What is the code");
            string code = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("What is the cost");
            decimal cost = decimal.Parse(Console.ReadLine());

            DentalService dentalService = new DentalService()
            {
                Name = name,
                Description = description,
                Code = code,
                Cost = cost
            };
            _dentalServiceService.Update(dentalService);
        }
    }
}
