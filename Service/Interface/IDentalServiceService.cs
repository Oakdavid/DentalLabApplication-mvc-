using DentalLabConsoleApplicationWithAdo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Service.Interface
{
    public interface IDentalServiceService
    {
        DentalService Create(DentalService dentalService);
        DentalService Update(DentalService dentalService);
        DentalService Get(int id);
        IEnumerable<DentalService> GetAllService();
        public void ToString(DentalService obj);
    }
}
