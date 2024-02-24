using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Repository.Interface
{
    public interface IProfileRepository
    {
        void Create(Profile profile);
        Profile Get(int id);
        public int GetProfileId();
        Profile GetProfileByUserId(int userId);
        Profile Get(string email);
        List<Profile> GetAll();
    }
}
