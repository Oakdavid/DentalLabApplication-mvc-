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
        IAppointmentRepository _appointmentRepository = new AppointmentRepository();
                                                                            
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
            var doctor = _doctorRepository.GetById(report.Id); 
            if (report != null && patient != null)
            {
                return new ReportDto
                {
                    ReportContent = report.ReportContent,
                    //DrName  = doctor.LicenseNumber, 
                };
            }
            return null;
        }

        public ReportDto GetReportByAppointmentId(int id)
        {
            var report = _reportRepository.GetReportByAppointmentId(id);
            var appointment = _appointmentRepository.GetById(report.Id);
            var profile = _profileRepository.GetProfileByUserId(Main.LoggedInId);
            var patient = _patientRepository.GetPatientByProfileId(profile.Id);
            //var doctor = _doctorRepository.GetById(report.Id);
            if (report != null && patient != null)
            {
                return new ReportDto
                {
                    ReportContent = report.ReportContent,
                    PatientComplaint = report.PatientComplain,
                    RefNumber = appointment.RefNumber,
                };
            }
            return null;
        }

        public ReportDto Update(ReportDto report)
        {
            var existingReportDto = _reportRepository.GetReportByAppointmentId(report.Id);
            if (existingReportDto != null)
            {

                existingReportDto.ReportContent = report.ReportContent;
                var reportRepo = _reportRepository.UpdateReport(existingReportDto);
                var reportDtos = new ReportDto
                {
                    ReportContent = reportRepo.ReportContent,
                    PatientComplaint = reportRepo.PatientComplain,
                };
                return reportDtos;
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
