using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Service.Interface
{
    public interface IUserService
    {
        UserDto Create(CreateUserRequestModel model);
        UserDto Get(string email);
        UserDto LoginByEmailAndPassword(LoginRequestModel requestModel);
        List<UserDto> GetAll();
    }
}
