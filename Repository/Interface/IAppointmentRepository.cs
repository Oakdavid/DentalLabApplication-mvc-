using DentalLabConsoleApplicationWithAdo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Repository.Interface
{
    public interface IAppointmentRepository
    {
        void Create(Appointment obj);
        Appointment Get(string refNumber);
        public Appointment GetByPatientNo(int patientNo);
        List<Appointment> GetAll();
        Appointment GetLastId();
    }
}
