using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Models;
using DentalLabConsoleApplicationWithAdo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Service.Interface
{
    public interface IAppointmentService
    {
        AppointmentDto Create(AppointmentRequestModel obj);
        AppointmentDto Get(string refNumber);
        AppointmentDto ViewAppointment(int patientNo);

        List<AppointmentDto> GetAll();
        void ToString(AppointmentDto obj);
        List<AppointmentDto> GetAllInitialized();
        public List<AppointmentDto> GetAppointmentByDoctorId(int id);
        bool AssignDoctorToAppointment(string reference, int doctorId);
        List<AppointmentDto> GetAllAssigned();
    }
}
