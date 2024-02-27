using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Menu;
using DentalLabConsoleApplicationWithAdo.Models.Entities;
using DentalLabConsoleApplicationWithAdo.Models.Enum;
using DentalLabConsoleApplicationWithAdo.Repository.Implementation;
using DentalLabConsoleApplicationWithAdo.Repository.Interface;
using DentalLabConsoleApplicationWithAdo.Service.Interface;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Service.Implementation
{
    public class DoctorService : IDoctorService
    {
        IDoctorRepository _doctorRepository = new DoctorRepository();
        IProfileRepository _profileRepository  = new ProfileRepository();
        IUserRepository _userRepository = new UserRepository();

        public DoctorDto Create(DoctorRequestRegistrationDto doctor)
        {
            var doctorExist = _doctorRepository.Get(doctor.LicenseNumber);
            if (doctorExist != null)
            {
                Console.WriteLine($"{doctor.LicenseNumber} already exist");
                return null;
            }

            User user = new User()
            {
                Email = doctor.Email,
                Password = doctor.Password,
                Role = "Doctor",
                IsDeleted = false
            };
            _userRepository.Create(user);
            

            Profile profile = new Profile
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Address = doctor.Address,
                Contact = doctor.Contact,
                DateOfBirth = doctor.DateOfBirth,
                Gender = Models.Enum.Gender.Male,
                UserId = _userRepository.GetById()
            };
            _profileRepository.Create(profile);



            Doctor doctors = new Doctor
            {
                LicenseNumber = doctor.LicenseNumber,
                Education = doctor.Education,
                YearsOfExperience = doctor.YearsOfExperience,
                Specializations = doctor.Specializations,
                SpecializationDescription = doctor.SpecializationDescription,
                ProfileId = _profileRepository.GetProfileId(),

            };
            _doctorRepository.Create(doctors);

            return new DoctorDto
            {
                LastName = doctor.LastName,
                Address = doctor.Address,
                Contact = doctor.Contact,
                Gender = Gender.Male,
                Email = doctor.Email,
                LicenseNumber = doctor.LicenseNumber,
                Education = doctor.Education,
                YearsOfExperience = doctor.YearsOfExperience,
                Specializations = doctor.Specializations,
                SpecializationDescription = doctor.SpecializationDescription
            };
        }

        public DoctorDto Get(string licenseNumber)
        {
            var doctor = _doctorRepository.Get(licenseNumber);
            var doctorProfile = _profileRepository.Get(doctor.ProfileId);
            if(doctor != null && !doctor.IsDeleted && doctorProfile != null )
            {
                
                return new DoctorDto
                {
                    LicenseNumber = doctor.LicenseNumber,
                    Education = doctor.Education,
                    YearsOfExperience = doctor.YearsOfExperience,
                    Specializations = doctor.Specializations,
                    SpecializationDescription = doctor.SpecializationDescription,
                    LastName = doctorProfile.LastName,
                };
            }
            return null;
        }

        public List<DoctorDto> GetAll()
        {
            var listOfDoctors = _doctorRepository.GetAll();
            var listOfDoctor = new List<DoctorDto>();
            foreach (var doctors in listOfDoctors)
            {
                var doctorProfile = _profileRepository.Get(doctors.ProfileId);    
                var doctorUser = _userRepository.Get(doctorProfile.UserId);      
                if (doctorProfile != null && doctorUser != null)  
                {

                    DoctorDto doctorDto = new DoctorDto
                    {

                        LastName = doctorProfile.LastName,
                        Email = doctorUser.Email,
                        Address = doctorProfile.Address,
                        Contact = doctorProfile.Contact,
                        Gender = Gender.Male,
                        LicenseNumber = doctors.LicenseNumber,
                        Education = doctors.Education,
                        YearsOfExperience = doctors.YearsOfExperience,
                        Specializations = doctors.Specializations,
                        SpecializationDescription = doctors.SpecializationDescription
                    };
                    listOfDoctor.Add(doctorDto);
                }
            }
            return listOfDoctor;
        }

        public List<DoctorDto> GetAllService()
        {
            var listOfDoctors = _doctorRepository.GetAll();
            var listOfDoctor = new List<DoctorDto>();
            foreach (var doctors in listOfDoctors)
            {
                DoctorDto doctorDto = new DoctorDto
                {
                    Id = doctors.Id,
                    Specializations = doctors.Specializations,
                    SpecializationDescription = doctors.SpecializationDescription
                };
                 listOfDoctor.Add(doctorDto);
            }
            return listOfDoctor;
        }

       
        public DoctorDto GetByEmail(string email) 
        {
            var doctor = _doctorRepository.GetByEmail(email);
            var doctorProfile = _profileRepository.Get(doctor.ProfileId);   
            if (doctor != null && !doctor.IsDeleted && doctorProfile != null)
            {

                return new DoctorDto
                {
                    LicenseNumber = doctor.LicenseNumber,
                    Education = doctor.Education,
                    YearsOfExperience = doctor.YearsOfExperience,
                    Specializations = doctor.Specializations,
                    SpecializationDescription = doctor.SpecializationDescription,
                    LastName = doctorProfile.LastName,
                };
            }
            return null;
        }

        public DoctorDto GetById(int id)
        {
            var doctor = _doctorRepository.GetById(id);
            var profile = _profileRepository.Get(doctor.ProfileId);
            var user = _userRepository.Get(profile.UserId);
            if (doctor != null && !doctor.IsDeleted)
            {
                return new DoctorDto
                {
                    LicenseNumber = doctor.LicenseNumber,
                    Education = doctor.Education,
                    YearsOfExperience = doctor.YearsOfExperience,
                    Specializations = doctor.Specializations,
                };
            }
            return null;
        }

        public bool Update(UpdateDoctorRequstRegistrationDto doctor)
        {
            var existingDoctor = _doctorRepository.Get(doctor.LicenseNumber);
            if (existingDoctor != null)
            {

                existingDoctor.Education = doctor.Education;
                existingDoctor.YearsOfExperience = doctor.YearsOfExperience;
                existingDoctor.Specializations = doctor.Specializations;
                existingDoctor.SpecializationDescription = doctor.SpecializationDescription;

                _doctorRepository.Update(existingDoctor);
                return true;
            }
            return false;
        }

        public List<DoctorDto> IsAvailable()
        {
            var availableDoctors = _doctorRepository.GetAllAvailableDoctors();
            if (availableDoctors != null)
            {
                return availableDoctors.Select(x => new DoctorDto
                {
                    LicenseNumber = x.LicenseNumber,
                    Education = x.Education,
                    YearsOfExperience = x.YearsOfExperience,
                    Specializations = x.Specializations,
                }).ToList();
            }
            return null;
        }


        public List<DoctorDto> GetDoctorSpecialization(DoctorDto doctor)
        {
            var specialization = _doctorRepository.GetDoctorSpecialization(doctor.Specializations);
            if (specialization != null)
            {
                return specialization.Select(x => new DoctorDto
                {
                    LicenseNumber = doctor.LicenseNumber,
                    Education = doctor.Education,
                    YearsOfExperience = doctor.YearsOfExperience,
                    Specializations = doctor.Specializations,

                }).ToList();
            }
            return null;
        }

        public void ToString(DoctorDto obj)
        {
            Console.WriteLine($"Id: {obj.Id}, Specialization: {obj.Specializations}, SpecializationDescription: {obj.SpecializationDescription}");
        }
    }
}
