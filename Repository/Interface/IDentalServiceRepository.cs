using DentalLabConsoleApplicationWithAdo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Repository.Interface
{
    public interface IDentalServiceRepository
    {
        DentalService Create(DentalService dentalService);
        DentalService Update(DentalService dentalService);
        //DentalService Update(int id);
        DentalService Get(int id);
        IEnumerable<DentalService> GetAllService();
    }
}
