﻿using DentalLabConsoleApplicationWithAdo.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Dto
{
    public class ProfileDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

    }

    public class ProfileRequestRegistrationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public DateTime DateOfBirth { get; set; }
        
        public string Gender { get; set; }
        public int UserId { get; set; }
    }
}
