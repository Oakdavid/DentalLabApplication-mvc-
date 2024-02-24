using DentalLabConsoleApplicationWithAdo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Repository.Interface
{
    public interface IUserRepository
    {
        void Create(User user);
        User Get(string email);
        User Get(int id);

        int GetById();
        User LoginByEmailAndPassword(string userEmail, string password);
        List<User> GetAll();
    }
}
