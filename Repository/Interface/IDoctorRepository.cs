using DentalLabConsoleApplicationWithAdo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Repository.Interface
{
    public interface IDoctorRepository
    {
        void Create(Doctor doctor);
        Doctor Get(string licenseNumber);
        Doctor GetByEmail(string email);
        Doctor GetById( int id);
        List<Doctor> GetAll();
        List<Doctor> GetAllAvailableDoctors();
        List<Doctor> GetDoctorSpecialization(string specialization);
        bool Update(Doctor doctor);

    }
}
