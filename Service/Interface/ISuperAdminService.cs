using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Service.Interface
{
    public interface ISuperAdminService
    {
        public void Create(string userName, string password, string role);
        public void UpdateUser(string userName, string password);
        public void DeleteUser(string userName);
    }
}
