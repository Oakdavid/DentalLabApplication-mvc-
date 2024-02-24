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
    public interface IPatientService
    {
        PatientDto Create(PatientRequestModelDto obj);
        PatientDto GetByCardNo(string cardNo);
        PatientDto Get(string email);
        PatientDto GetPatientId(int id);
        List<PatientDto> GetAll();
        public void ToString(PatientDto patient);

    }
}
