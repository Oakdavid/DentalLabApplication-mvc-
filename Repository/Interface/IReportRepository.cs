using DentalLabConsoleApplicationWithAdo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Repository.Interface
{
    public interface IReportRepository
    {
        void Create(Report report);
        Report Get(string refNumber);
        public Report GetByCardNo(string cardNo);
        Report GetByEmail(string email);
        List<Report> GetAll();
        bool Delete(string refNumber);

    }
}
