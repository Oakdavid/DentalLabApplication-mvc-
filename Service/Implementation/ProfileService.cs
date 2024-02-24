using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Menu;
using DentalLabConsoleApplicationWithAdo.Models;
using DentalLabConsoleApplicationWithAdo.Models.Entities;
using DentalLabConsoleApplicationWithAdo.Models.Enum;
using DentalLabConsoleApplicationWithAdo.Repository.Implementation;
using DentalLabConsoleApplicationWithAdo.Repository.Interface;
using DentalLabConsoleApplicationWithAdo.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Service.Implementation
{
    public class ProfileService : IProfileService
    {
        IProfileRepository _profileRepository = new ProfileRepository();
        IUserRepository _userRepository = new UserRepository();

        public ProfileDto Create(ProfileRequestRegistrationModel model)
        {
            Profile profile = new Profile()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Contact = model.Contact,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                UserId = _userRepository.GetById()
            };
            _profileRepository.Create(profile);

            return new ProfileDto
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Address = profile.Address,
                Contact = profile.Contact,
                Gender = profile.Gender,
            };
        }

        public ProfileDto Get(string email)
        {
            var profile = _profileRepository.Get(email);
            if(profile != null && !profile.IsDeleted)
            {
                return new ProfileDto
                {
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Address = profile.Address,
                    Contact = profile.Contact,
                };
            }
            return null;
        }

        public List<ProfileDto> GetAll()
        {
            var profile = _profileRepository.GetAll();
            List<ProfileDto> profileDtos = new List<ProfileDto>();
            foreach (var item in profile)
            {
                ProfileDto profileDto = new ProfileDto
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Address = item.Address,
                    Contact = item.Contact,
                };
                profileDtos.Add(profileDto);
            }
            return profileDtos;
        }
    }
}
