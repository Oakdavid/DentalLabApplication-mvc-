using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Service.Interface
{
    public interface IDoctorService
    {
        DoctorDto Create(DoctorRequestRegistrationDto doctor);
        DoctorDto Get(string LicenseNumber);
        DoctorDto GetByEmail(string email);
        List<DoctorDto> GetAll();
        DoctorDto GetById(int id);
        List<DoctorDto> GetAllService();
        bool Update(UpdateDoctorRequstRegistrationDto doctor);
        public void ToString(DoctorDto obj);
    }
}
