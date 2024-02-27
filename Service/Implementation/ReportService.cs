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
        IProfileRepository _profileRepository = new ProfileRepository();      
        IPatientRepository _patientRepository = new PatientRepository(); 
        IDoctorRepository _doctorRepository = new DoctorRepository();
                                                                            
        public ReportDto Create(ReportRequestModelDto reports)
        {
            var profile = _profileRepository.GetProfileByUserId(Main.LoggedInId);
            var patient = _patientRepository.GetPatientByProfileId(profile.Id);     // am having exception
            
            var report = _reportRepository.Get(reports.RefNumber);

            if (report != null)
            {
                Console.WriteLine($"{report} already exist");
                return null;
            }

            Report newReport = new Report()
            {
                ReportContent = reports.ReportContent,     // more work needs to be done
            };
            _reportRepository.Create(newReport);

            return new ReportDto
            {
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
            var profile = _profileRepository.GetProfileByUserId(Main.LoggedInId);
            var patient = _patientRepository.GetPatientByProfileId(profile.Id);
            var doctor = _doctorRepository.GetById(report.Id); // not too sure if it will get it
            if (report != null && patient != null)
            {
                return new ReportDto
                {
                    ReportContent = report.ReportContent,
                    DrName  = doctor.LicenseNumber,                  // report.DrName, // i still need to include the doctors name
                    
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
                    ReportContent = report.ReportContent,
                };
                reportDtos.Add(reportDto);
            }
            return reportDtos;
        }
    }
}
