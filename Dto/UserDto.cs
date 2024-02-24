using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Role { get; set; }    
    }

    public class CreateUserRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role {  get; set; }
    }

    public class LoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
