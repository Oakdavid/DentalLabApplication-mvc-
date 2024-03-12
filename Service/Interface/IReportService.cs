using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Models;
using DentalLabConsoleApplicationWithAdo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Service.Interface
{
    public interface IReportService
    {
        ReportDto Create(ReportRequestModelDto report);
        ReportDto Get(string refNumber);
        List<ReportDto> GetAll();
        bool Delete(string refNumber);
        ReportDto GetReportByAppointmentId(int id);
        ReportDto Update(ReportDto report);
    }
}
