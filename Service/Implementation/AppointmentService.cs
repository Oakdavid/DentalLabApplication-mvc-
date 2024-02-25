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
        public AppointmentDto Create(AppointmentRequestModel obj)
        {
            Appointment appointment = new Appointment
            {

                RefNumber = $"RDT/DENTAL/00/{new Random().Next(001, 100)}",
                CardNo = obj.CardNo,
                DrNumber = $"RDT/DOCTOR/{new Random().Next(3, 5)}",
                PatientComplain = obj.PatientComplain,
                DateOfAppointment = DateTime.UtcNow,
                AppointmentStatus = AppointmentStatus.Initialized,
                AppointmentType = AppointmentType.PhysicalAppointment,
                IsDeleted = false
                
                //ReportContent = obj.ReportContent,
            };
            _appointmentRepository.Create(appointment);

            return new AppointmentDto
            {
                Id = appointment.Id,
                RefNumber = appointment.RefNumber,
                CardNo = appointment.CardNo,
                DrNumber = appointment.DrNumber,
                PatientComplain = appointment.PatientComplain,
                AppointmentStatus = appointment.AppointmentStatus
                
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
                    CardNo = appointment.CardNo,
                    DrNumber = appointment.DrNumber,
                    PatientComplain = appointment.PatientComplain,
                    DateOfAppointment = appointment.DateOfAppointment
                };
            }
            Console.WriteLine($"Appointment with reference no {refNumber} does not exist.");
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
                    CardNo = appointment.CardNo,
                    PatientComplain = appointment.PatientComplain,
                    DateOfAppointment = appointment.DateOfAppointment,
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
                    DateOfAppointment = appointment.DateOfAppointment
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
