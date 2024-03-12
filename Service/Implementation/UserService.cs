using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Models.Entities;
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
    public class UserService : IUserService
    {
        IUserRepository _userRepository = new UserRepository();
        public UserDto Create(CreateUserRequestModel model)
        {
            var userExist = _userRepository.Get(model.Email);
            if (userExist != null)
            {
                Console.WriteLine($"{userExist}  exist");
                return null;
            }

            User user = new User()
            {
                Email = model.Email,
                Password = model.Password,
                Role = model.Role,
                
                
            };
            _userRepository.Create(user);
            return new UserDto
            {
                Email = user.Email,
                Role =  user.Role,
                Id = user.Id
            };
        }

        public UserDto Get(string email)
        {
            var userGet = _userRepository.Get(email);
            if( userGet == null )
            {
                Console.WriteLine($"{userGet.Email} not existed");
            }
            return new UserDto
            {
                Email = userGet.Email,
                Role = userGet.Role
            };
        }

        public List<UserDto> GetAll()
        {
            var userList = _userRepository.GetAll();
            var users = userList.Select(user => new UserDto
            {
                Email = user.Email,
                Role= user.Role
            }).ToList();
            return users;
        }

        public UserDto LoginByEmailAndPassword(LoginRequestModel requestModel)
        {
            try
            {
                var userLogin = _userRepository.LoginByEmailAndPassword(requestModel.Email, requestModel.Password);
                if(userLogin == null )
                {
                    Console.WriteLine($"{requestModel.Email} or Password is not recognised \nEnter the correct credentials");
                    return null;
                }
            
                return new UserDto
                {
                    Email = userLogin.Email,
                    Role = userLogin.Role,
                    Id = userLogin.Id
                };
            }
            catch ( Exception ex )
            {
                Console.WriteLine($" error: {ex.Message}" );
                return null;
            }
        }
    }
}
