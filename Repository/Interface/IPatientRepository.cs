using DentalLabConsoleApplicationWithAdo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Repository.Interface
{
    public interface IPatientRepository
    {
        void Create(Patient obj);
        Patient GetByCardNo(string cardNo);
        Patient GetById(int userId);
        Patient GetPatientByProfileId(int id);
        List<Patient> GetAll();
    }
}
