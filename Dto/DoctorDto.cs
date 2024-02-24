using DentalLabConsoleApplicationWithAdo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Dto
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string LicenseNumber { get; set; }
        public string Education { get; set; }
        public int YearsOfExperience { get; set; }
        public string Specializations { get; set; }
        public string SpecializationDescription { get; set; }

    }

    public class DoctorRequestRegistrationDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        [Required(ErrorMessage = "Date of Birth is required")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LicenseNumber { get; set; }
        public string Education { get; set; }
        public int YearsOfExperience { get; set; }
        public string Specializations { get; set; }
        public string SpecializationDescription { get; set; }
    }

    public class UpdateDoctorRequstRegistrationDto
    {
        public string LicenseNumber { get; set; }
        public string Education { get; set; }
        public int YearsOfExperience { get; set; }
        public string Specializations { get; set; }
        public string SpecializationDescription { get; set; }
    }
}

