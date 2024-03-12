using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Models.Entities;
using DentalLabConsoleApplicationWithAdo.Service.Implementation;
using DentalLabConsoleApplicationWithAdo.Service.Interface;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
            Console.WriteLine("Enter 1 to view all Patients\nEnter 2 to view all available Doctors\nEnter 3 to assign Doctor\nEnter 4 to view assigned Patient\nEnter 5 to Create Dental Services \nEnter 6 to Update Dental Service\nEnter 0 to log out");
            string options = Console.ReadLine();
            if (options == "1")
            {
                ViewAllPatient();               
                HeadDoctor();
            }
            else if (options == "2")
            {
                AllAvailableDoctors();             
                HeadDoctor();
            }
            else if (options == "3")
            {
                AssignDoctors();
                HeadDoctor();
            }
            else if (options == "4")
            {
                ViewAllAssignedPatient();
                HeadDoctor();
            }
            else if (options == "5")
            {
                DentalServices();
                HeadDoctor();
            }
            else if (options == "6")
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
            if (availableDoctor == null || availableDoctor.Count == 0)
            {
                Console.WriteLine("No available doctors");
                return;
            }
            foreach (var doctor in availableDoctor)
            {
                Console.WriteLine($"License: {doctor.LicenseNumber}\tEducation:{doctor.Education}\tYears of Experience:{doctor.YearsOfExperience}\tSpecialization:{doctor.Specializations}");
            }

        }       

        public void DentalServices()            
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

        public void UpdateDentalService()           
        {
            Console.WriteLine("What is the id");
            int id = int.Parse(Console.ReadLine());

            var dentalServiceToUpdate = _dentalServiceService.Get(id);
            if(dentalServiceToUpdate != null)
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

                dentalServiceToUpdate.Name = name;
                dentalServiceToUpdate.Description = description;
                dentalServiceToUpdate.Code = code;
                dentalServiceToUpdate.Cost = cost;
                _dentalServiceService.Update(dentalServiceToUpdate);
                Console.WriteLine("Dental service updated successfully");
            }
            else
            {
                Console.WriteLine("Dental service not found");
            }
        }

        public void AssignDoctors()
        {
            Console.WriteLine("List of All un-assigned appointment");
            var getDoctor = _appointmentService.GetAllInitialized();
            foreach (var item in getDoctor)
            {
                Console.WriteLine($"Id = {item.Id}\tPatient Name: {item.PatientName}\tPatient RefNumber:{item.RefNumber}");
            }

            Console.WriteLine("List of All available doctor");
            var doctors = _doctorService.IsAvailable();
            foreach (var doc in doctors)
            {
                Console.WriteLine($"Doctor Id:{doc.Id}\tDoctor Last Name{doc.LastName}");
            }
            Console.WriteLine();
            Console.WriteLine("select appointment ref number and doctor Id");
            Console.WriteLine("Enter patient Ref");
            string referenceNumber = Console.ReadLine();
            Console.WriteLine("Enter doctorId");
            int Id = int.Parse(Console.ReadLine());
            _appointmentService.AssignDoctorToAppointment(referenceNumber, Id);
            Console.WriteLine("doctor assigned successfully");
        }

        public void ViewAllAssignedPatient()
        {
            var assingedPatients = _appointmentService.GetAllAssigned();
            if (assingedPatients != null)
            {
                foreach (var assign in assingedPatients)
                {
                    Console.WriteLine($"Doctor Id: {assign.DoctorId}\tCard No:{assign.CardNo}\tAppointment Status:{assign.AppointmentStatus}\tDate of Appointment: {assign.DateOfAppointment}");
                }
            }
        }
    }
}
