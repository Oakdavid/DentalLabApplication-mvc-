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
        List<Report> GetAll();
        bool Delete(string refNumber);
        Report GetReportByAppointmentId(int id);
        Report UpdateReport(Report updatedReport);
        Report GetAppointmentById(int id);
    }
}
