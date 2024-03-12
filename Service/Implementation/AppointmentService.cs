using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Menu;
using DentalLabConsoleApplicationWithAdo.Models.Entities;
using DentalLabConsoleApplicationWithAdo.Models.Enum;
using DentalLabConsoleApplicationWithAdo.Repository.Implementation;
using DentalLabConsoleApplicationWithAdo.Repository.Interface;
using DentalLabConsoleApplicationWithAdo.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace DentalLabConsoleApplicationWithAdo.Service.Implementation
{
    public class AppointmentService : IAppointmentService
    {
        IAppointmentRepository _appointmentRepository = new AppointmentRepository();
        IPatientRepository _patientRepository = new PatientRepository();
        IReportRepository _reportRepository = new ReportRepository();
        IProfileRepository _profileRepository = new ProfileRepository();
        IDoctorRepository _doctorRepository = new DoctorRepository();    
        public AppointmentDto Create(AppointmentRequestModel obj)
        {
            var patientObj = _patientRepository.GetById(obj.PatientId);
            if (patientObj == null)
            {
                Console.WriteLine("No object found");
                return null;
            }
            Appointment appointment = new Appointment
            {

                RefNumber = $"RDT/DENTAL/00/{new Random().Next(001, 100)}",
                DateOfAppointment = DateTime.Now,
                AppointmentStatus = AppointmentStatus.Initialized,
                AppointmentType = AppointmentType.PhysicalAppointment,
                IsDeleted = false,
                Patient = patientObj,
                PatientId = patientObj.Id,
                DoctorId = patientObj.Id,
            };
            _appointmentRepository.Create(appointment);
            var lastAppointmentId = _appointmentRepository.GetLastId();
            var report = new Report()
            {
                AppointmentId = lastAppointmentId.Id,
                Appointment = appointment,
                PatientComplain = obj.PatientComplain
            };

            _reportRepository.Create(report);

            return new AppointmentDto
            {
                Id = appointment.Id,
                RefNumber = appointment.RefNumber,
                AppointmentStatus = appointment.AppointmentStatus,
                AppointmentType = appointment.AppointmentType,
                CardNo = obj.CardNo,
                PatientComplain = obj.PatientComplain,
                DateOfAppointment = DateTime.Now,
                DrNumber = obj.DrNumber,

            };
        }

        public AppointmentDto Get(string refNumber)
        {
            var appointment = _appointmentRepository.Get(refNumber);

            if (appointment != null && !appointment.IsDeleted)
            {
                return new AppointmentDto
                {
                    Id = appointment.Id,
                    AppointmentStatus = appointment.AppointmentStatus,
                    AppointmentType = AppointmentType.VirtualAppointment,
                    DateOfAppointment = appointment.DateOfAppointment,
                    RefNumber = refNumber,
                };
            }
            Console.WriteLine($"Appointment with reference no: {refNumber} does not exist. Please proceed to book your appointment");
            return null;
        }

        public List<AppointmentDto> GetAll()
        {
            var appointmentGetAll = _appointmentRepository.GetAll();
            var listOfAppointmentDtos = new List<AppointmentDto>();
            foreach (var appointment in appointmentGetAll)
            {
                var appointmentDto = new AppointmentDto
                {
                    Id = appointment.Id,
                    RefNumber = appointment.RefNumber,
                    AppointmentStatus = appointment.AppointmentStatus,
                    DateOfAppointment = appointment.DateOfAppointment,
                    AppointmentType = AppointmentType.VirtualAppointment,

                };
                listOfAppointmentDtos.Add(appointmentDto);
            }
            return listOfAppointmentDtos;
        }
        public List<AppointmentDto> GetAllInitialized()
        {
            var appointmentGetAll = _appointmentRepository.GetAllInitialized();
            var listOfAppointmentDtos = new List<AppointmentDto>();
            foreach (var appointment in appointmentGetAll)
            {
                var appointmentDto = new AppointmentDto
                {
                    Id = appointment.Id,
                    RefNumber = appointment.RefNumber,
                    AppointmentStatus = appointment.AppointmentStatus,
                    DateOfAppointment = DateTime.Now,
                    AppointmentType = AppointmentType.VirtualAppointment,
                    PatientId = appointment.PatientId,
                    PatientName = _profileRepository.Get(_patientRepository.GetById(appointment.PatientId.Value).ProfileId).FirstName + " " + _profileRepository.Get(_patientRepository.GetById(appointment.PatientId.Value).ProfileId).LastName
                };
                listOfAppointmentDtos.Add(appointmentDto);
            }
            return listOfAppointmentDtos;
        }

        public List<AppointmentDto> GetAllAssigned()
        {
            var appointmentGetAll = _appointmentRepository.GetAllAssignedAppointment();
            var listOfAppointmentDtos = new List<AppointmentDto>();
            foreach (var appointment in appointmentGetAll)
            {
                var appointmentDto = new AppointmentDto
                {
                    Id = appointment.Id,
                    RefNumber = appointment.RefNumber,
                    AppointmentStatus = appointment.AppointmentStatus,
                    DateOfAppointment = DateTime.Now,
                    AppointmentType = appointment.AppointmentType,
                    PatientId = appointment.PatientId,
                    PatientName = _profileRepository.Get(_patientRepository.GetById(appointment.PatientId.Value).ProfileId).FirstName + " " + _profileRepository.Get(_patientRepository.GetById(appointment.PatientId.Value).ProfileId).LastName //.Value is used to access the actual integer value stored in the nullable type.
                };
                listOfAppointmentDtos.Add(appointmentDto);
            }
            return listOfAppointmentDtos;
        }

        public List<AppointmentDto> GetAppointmentByDoctorId(int id)
        {
            var appointmentGetAll = _appointmentRepository.GetAppointmentByDoctorId(id);
            var listOfAppointmentDtos = new List<AppointmentDto>();
            foreach (var appointment in appointmentGetAll)
            {
                var appointmentDto = new AppointmentDto
                {
                    Id = appointment.Id,
                    RefNumber = appointment.RefNumber,
                    AppointmentStatus = appointment.AppointmentStatus,
                    DateOfAppointment = DateTime.Now,
                    AppointmentType = appointment.AppointmentType,
                    PatientId = appointment.PatientId,
                    DoctorId = appointment.DoctorId,
                    PatientName = _profileRepository.Get(_patientRepository.GetById(appointment.PatientId.Value).ProfileId).FirstName + " " + _profileRepository.Get(_patientRepository.GetById(appointment.PatientId.Value).ProfileId).LastName
                };
                listOfAppointmentDtos.Add(appointmentDto);
            }
            return listOfAppointmentDtos;
        }

        public AppointmentDto ViewAppointment(int patientNo)
        {
            var appointment = _appointmentRepository.GetByPatientNo(patientNo);
            if (appointment != null && !appointment.IsDeleted)
            {
                return new AppointmentDto
                {
                    Id = appointment.Id,
                    AppointmentStatus = appointment.AppointmentStatus,
                    DateOfAppointment = appointment.DateOfAppointment,
                    AppointmentType=appointment.AppointmentType
                };

            }
            else
            {
                 Console.WriteLine($"No appointment found proceed to book appointment");
            }
            return null;
        }

        public bool AssignDoctorToAppointment(string reference, int doctorId)
        {
            var appointment = _appointmentRepository.Get(reference);
            var doctor = _doctorRepository.GetById(doctorId);
            if (appointment == null && doctor == null)
            {
                return false;
            }
            var app = new Appointment
            {
                DoctorId = doctor.Id,
                Id = appointment.Id,
                AppointmentStatus= AppointmentStatus.Assigned,
            };
            var doc = new Doctor
            {
                Id = doctor.Id,
                IsAvailable = false
            };
            _appointmentRepository.UpdateAppointmentWithDoctorId(app);
            _doctorRepository.UpdateDoctorStatus(doc);
            return true;
        }

        public void ToString(AppointmentDto obj)
        {
            Console.WriteLine($"Id: {obj.Id}\t RefNUmber: {obj.RefNumber}\t CardNo: {obj.CardNo}\t PatientComplain: {obj.PatientComplain}\t Date of Appointment: {obj.DateOfAppointment}");

        }
    }
}
