using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Service.Interface
{
    public interface IProfileService
    {
        ProfileDto Create(ProfileRequestRegistrationModel profile);
        ProfileDto Get(string email);
       // ProfileDto GetProfileId(int id);
        List<ProfileDto> GetAll();
    }
}
