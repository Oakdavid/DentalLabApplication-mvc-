using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Models.Entities;
using DentalLabConsoleApplicationWithAdo.Repository.Implementation;
using DentalLabConsoleApplicationWithAdo.Repository.Interface;
using DentalLabConsoleApplicationWithAdo.Service.Interface;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using iTextSharp.text.pdf;
using System.Threading.Tasks;
using System.Security.Cryptography;
using DentalLabConsoleApplicationWithAdo.Menu;

namespace DentalLabConsoleApplicationWithAdo.Service.Implementation
{
    public class ReportService : IReportService
    {
        IReportRepository _reportRepository = new ReportRepository();
        IProfileRepository _profileRepository = new ProfileRepository();           // coming back to check this
        IPatientRepository _patientRepository = new PatientRepository();           // coming back to check this
                                                                                // coming back to check this
        public ReportDto Create(ReportRequestModelDto reports)
        {
            var profile = _profileRepository.GetProfileByUserId(Main.LoggedInId);
            var patient = _patientRepository.GetPatientByProfileId(profile.Id);
            
            var report = _reportRepository.Get(reports.RefNumber);

            if (report != null)
            {
                Console.WriteLine($"{report} already exist");
                return null;
            }

            Report newReport = new Report()
            {
                ReportContent = reports.ReportContent,
                PatientCardNo = patient.CardNo
            };
            _reportRepository.Create(newReport);

            return new ReportDto
            {
                RefNumber = newReport.RefNumber,
                DrName = newReport.DrName,
                PatientCardNo = newReport.PatientCardNo,
                ReportContent = newReport.ReportContent,
            };
        }
        public bool Delete(string refNumber)
        {
            var deletedReport = _reportRepository.Get(refNumber);
            if(deletedReport != null)
            {
                deletedReport.IsDeleted = true;
                Console.WriteLine($"{deletedReport.Id} deleted successful");
                return true;
            }
            else
            {
                Console.WriteLine($"{deletedReport.Id} deleted unsuccessful");
                return false;
            }
        }

        public ReportDto Get(string cardNo)
        {
           var report = _reportRepository.GetByCardNo(cardNo);
           if(report != null)
           {
                return new ReportDto
                {
                    ReportContent = report.ReportContent,
                    DrName  = report.DrName, // i still need to include the doctors name
                    PatientCardNo= report.PatientCardNo,
                    RefNumber = report.RefNumber
                };
           }
            return null;

        }

        public List<ReportDto> GetAll()
        {
            var reportList = _reportRepository.GetAll();
            List<ReportDto> reportDtos = new List<ReportDto>();
            foreach( var report in reportList)
            {
                ReportDto reportDto = new ReportDto()
                {
                    RefNumber = report.RefNumber,
                    PatientCardNo = report.PatientCardNo,
                    ReportContent = report.ReportContent,
                    DrName = report.DrName,
                };
                reportDtos.Add(reportDto);
            }
            return reportDtos;
        }
    }
}
