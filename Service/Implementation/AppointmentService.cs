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
        public AppointmentDto Create(AppointmentRequestModel obj)
        {
            var patientObj = _patientRepository.GetById(obj.PatientId);
            if(patientObj ==null)
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
                Patient = patientObj,       // to get object of the patient
                PatientId = patientObj.Id,
                DoctorId = patientObj.Id,
                
                //ReportContent = obj.ReportContent,
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
                DateOfAppointment= DateTime.Now,
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
                    DateOfAppointment = DateTime.Now,
                    RefNumber = refNumber,
                };
            }
            Console.WriteLine($"Appointment with reference no: {refNumber} does not exist.");
            return null;
        }

        public List<AppointmentDto> GetAll()
        {
            var appointmentGetAll = _appointmentRepository.GetAll();
            var  listOfAppointmentDtos = new List<AppointmentDto>();
            foreach ( var appointment in appointmentGetAll )
            {
                var appointmentDto = new AppointmentDto
                {
                    Id = appointment.Id,
                    RefNumber = appointment.RefNumber,
                    AppointmentStatus = appointment.AppointmentStatus,
                    DateOfAppointment = DateTime.Now,
                    AppointmentType= AppointmentType.VirtualAppointment,
                    
                };
                listOfAppointmentDtos.Add(appointmentDto);
            }
            return listOfAppointmentDtos;
        }

        public List<AppointmentDto> GetAllWithoutAppointment()          // without appointment  select id witht responce and give a response
        {
            var appointmentGetAll = _appointmentRepository.GetAll();
            var listOfAppointmentDtos = new List<AppointmentDto>();
            foreach (var appointment in appointmentGetAll)
            {
                var appointmentDto = new AppointmentDto
                {
                    Id = appointment.Id,
                    RefNumber = appointment.RefNumber,
                };
                listOfAppointmentDtos.Add(appointmentDto);
            }
            return listOfAppointmentDtos;
        }

        public AppointmentDto ViewAppointment(string patientCardno)
        {
            var appointment = _appointmentRepository.GetByCardNo(patientCardno);
            if (appointment != null && !appointment.IsDeleted)
            {
                return new AppointmentDto
                {
                    Id = appointment.Id,
                };

            }
            Console.WriteLine($"Appointment with reference no {patientCardno} does not exist.");
            return null;
        }
        public void ToString(AppointmentDto obj)
        {
            Console.WriteLine($"Id: {obj.Id}\t RefNUmber: {obj.RefNumber}\t CardNo: {obj.CardNo}\t PatientComplain: {obj.PatientComplain}\t Date of Appointment: {obj.DateOfAppointment}");
           
        }
    }
}
