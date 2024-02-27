using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Menu;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DentalLabConsoleApplicationWithAdo.Service.Implementation
{
    public class PatientService : IPatientService
    {
        IPatientRepository _patientRepository = new PatientRepository();
        IUserRepository _userRepository = new UserRepository();
        IProfileRepository _profileRepository = new ProfileRepository();
        IDoctorRepository  _doctorRepository = new DoctorRepository();

        public PatientDto Create(PatientRequestModelDto obj)
        {
            var patientExist = _patientRepository.GetByCardNo(obj.PatientCardNo); 
            if (patientExist != null)
            {
                Console.WriteLine($"{patientExist} exist");
                return null;
            }
            User user = new User
            {
                Email = obj.Email,
                Password = obj.Password,
                Role = "Patient",
                IsDeleted = false,
            };
            _userRepository.Create(user);

            Profile profile = new Profile()
            {
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Address = obj.Address,
                Contact = obj.Contact,
                DateOfBirth = obj.DateOfBirth,
                Gender = Gender.Male,
                UserId = _userRepository.GetById()
            };
            _profileRepository.Create(profile);

            var CardNo = $"RDT/CARDNO/00/{new Random().Next(50, 100)}";            
            Patient patient = new Patient()
            {
                CardNo = CardNo,
                ProfileId = _profileRepository.GetProfileId(),
                DrLicenseNumber = AssignedDoctor(CardNo)
            };
            _patientRepository.Create(patient);

            return new PatientDto
            {
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Address = obj.Address,
                Contact = obj.Contact,
                DateOfBirth = obj.DateOfBirth,
                Gender = Gender.Male,
                PatientCardNo = obj.PatientCardNo,
            };

        }

        public PatientDto Get(string PatientCardNo)
        {
            var patient = _patientRepository.GetByCardNo(PatientCardNo);
            if(patient != null  && !patient.IsDeleted)
            {
                var patientProfile = _profileRepository.Get(patient.ProfileId);
                var patientUser = _userRepository.Get(patientProfile.UserId);
                if(patientProfile != null)
                {
                    return new PatientDto
                    {
                        PatientCardNo = patient.CardNo,
                        FirstName = patientProfile.FirstName,
                        LastName = patientProfile.LastName,
                        Address = patientProfile.Address,
                        Contact = patientProfile.Contact,
                        DateOfBirth = patientProfile.DateOfBirth,
                        Gender = Gender.Male,
                        LicenseNumber = patient.DrLicenseNumber,
                    };
                }
            }
            return null;
        }

        public List<PatientDto> GetAll()
        {
            var patientList = _patientRepository.GetAll();
            var patientDtosList = new List<PatientDto>();
            foreach (var patient in patientList)
            {
                var patientProfile = _profileRepository.Get(patient.ProfileId);
                var patientUser = _userRepository.Get(patientProfile.UserId); 
                if (patientUser != null && patientProfile != null)
                {
                    PatientDto patientDto = new PatientDto      //maping of patient and profile, user
                    {

                        PatientCardNo = patient.CardNo,
                        FirstName = patientProfile.FirstName,
                        LastName = patientProfile.LastName,
                        Address = patientProfile.Address,
                        Contact = patientProfile.Contact,
                        DateOfBirth = patientProfile.DateOfBirth,
                        Gender = Gender.Male,
                        LicenseNumber = patient.DrLicenseNumber,
                    };
                    patientDtosList.Add(patientDto);
                }
            }
            return patientDtosList;
        }

        public PatientDto GetByCardNo(string cardNo)
        {
            var patientCardNo = _patientRepository.GetByCardNo(cardNo);
            var profile = _profileRepository.Get(patientCardNo.ProfileId);
            var user = _userRepository.Get(profile.UserId);
            if(patientCardNo != null && !patientCardNo.IsDeleted)
            {
                return new PatientDto
                {
                    PatientCardNo = patientCardNo.CardNo,
                    Address = profile.Address,
                    Contact = profile.Contact,
                    DateOfBirth = profile.DateOfBirth,
                    Gender = Gender.Male,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    LicenseNumber = patientCardNo.DrLicenseNumber,
                };
            }
            return null;
        }

        public PatientDto GetPatientId(int id)
        {
            var profile = _profileRepository.GetProfileByUserId(id);
            var patientUserId = _patientRepository.GetPatientByProfileId(profile.Id);
            var user = _userRepository.Get(profile.UserId);
            if(patientUserId != null)
            {
                return new PatientDto
                {
                    Id = patientUserId.Id,
                    PatientCardNo = patientUserId.CardNo,
                    Address = profile.Address,
                    Contact = profile.Contact,
                    DateOfBirth = profile.DateOfBirth,
                    Gender = Gender.Male,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    LicenseNumber = patientUserId.DrLicenseNumber
                };
            }
            return null;
        }

        //private string AssignedDoctor(string cardNo)        // asigning a doctor randomling
        //{
        //    var getDoctors = _doctorRepository.GetAll();
        //    var patients = _patientRepository.GetByCardNo(cardNo);
        //    if(patients.DrLicenseNumber == null)
        //    {
        //        var doctorAtIndex = new Random().Next(0, getDoctors.Count());
        //        var assignedDoctor = getDoctors[doctorAtIndex];
        //        return assignedDoctor.LicenseNumber.ToString();
        //    }
        //    else
        //    {
        //        Console.WriteLine("No available Doctor");
        //    }
        //    return null;
        //}

        private string AssignedDoctor(string cardNo)
        {
            var doctor = _doctorRepository.GetAll();
            var patient = _patientRepository.GetByCardNo(cardNo);
            if (patient == null)
            {
                Console.WriteLine("Patient not found");
                return null;
            }

            if (string.IsNullOrEmpty(patient.DrLicenseNumber) && doctor.Any()) // it check if doctor is null or empty the any is a link method
            {
                var randomIndex = new Random().Next(0, doctor.Count());
                return doctor[randomIndex].LicenseNumber.ToString();
            }
            Console.WriteLine("No available doctor");
            return null;
        }

        public  void ToString(PatientDto patient)
        {
            Console.WriteLine($"PatientCardNo: {patient.PatientCardNo}\t First Name: {patient.FirstName}\t Last Name: {patient.LastName}\t " +
                   $"Address: {patient.Address}\t Contact: {patient.Contact}\t Date of Birth: {patient.DateOfBirth}\t Gender: {patient.Gender}\t");
            
        }
    }
}
