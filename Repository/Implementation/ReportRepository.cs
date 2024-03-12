using DentalLabConsoleApplicationWithAdo.Models.Entities;
using DentalLabConsoleApplicationWithAdo.Repository.Interface;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DentalLabConsoleApplicationWithAdo.Repository.Implementation
{
    internal class ReportRepository : IReportRepository
    {
        public void Create(Report report)
        {
            var refNumber = $"RDT/DENTAL/00/{new Random().Next(001, 100)}";
            var tinyDeleted = report.IsDeleted ? 1 : 0;
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                string insertQuery = $" insert into Report(AppointmentId, ReportContent, PatientComplain, IsDeleted) " +
                $" VALUES('{report.AppointmentId}', '{report.ReportContent}', '{report.PatientComplain}', '{tinyDeleted}')";
                var command = new MySqlCommand(insertQuery, conn);       // just added

                var input = command.ExecuteNonQuery();
                if(input > 0)
                {
                   // Console.WriteLine("Report created successfully");
                }
            }
        }

        public bool Delete(string refNumber)
        {
            using(MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                string query = $"delete from report where RefNumber = '{refNumber}'";
                var command = new MySqlCommand(@query, conn);
                var deleteReport = command.ExecuteNonQuery();
                if(deleteReport > 0)
                {
                    Console.WriteLine("Record deleted successfully");
                    return true;
                }
                else
                {
                     Console.WriteLine("No record found");
                    return false;
                }
            }
        }

        public Report Get(string refNumber)
        {
            using(MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                string query = $" select * from report where RefNumber = '{refNumber}'";
                var command = new MySqlCommand(query, conn);
                var reportReader = command.ExecuteReader();
                while(reportReader.Read())
                {
                    return new Report
                    {
                        Id = (int)reportReader["Id"],
                        ReportContent = reportReader["ReportContent"].ToString(),
                        IsDeleted = Convert.ToBoolean(reportReader["IsDeleted"]),
                    };
                }
            }
            return null;
        }

        public Report GetReportByAppointmentId(int id)
        {
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                string query = $" select * from report where Id = '{id}'";
                var command = new MySqlCommand(query, conn);
                var reportReader = command.ExecuteReader();
                while (reportReader.Read())
                {
                    return new Report
                    {
                        Id = (int)reportReader["Id"],
                        ReportContent = reportReader["ReportContent"].ToString(),
                        PatientComplain = reportReader["PatientComplain"].ToString(),
                        
                        IsDeleted = Convert.ToBoolean(reportReader["IsDeleted"]),
                    };
                }
            }
            return null;
        }
        public Report UpdateReport(Report updatedReport)
        {
            using (MySqlConnection conn = new MySqlConnection(DentalLabDbContext.connections))
            {
                conn.Open();
                string query = $"UPDATE report SET ReportContent = '{updatedReport.ReportContent}', PatientComplain = '{updatedReport.PatientComplain}', IsDeleted = '{updatedReport.IsDeleted}' WHERE Id = '{updatedReport.Id}'";

                var command = new MySqlCommand(query, conn);

                var reportUpdate = command.ExecuteNonQuery();
                if (reportUpdate > 0)
                {
                    new Report
                    {
                        ReportContent = updatedReport.ReportContent,
                        PatientComplain = updatedReport.PatientComplain,
                    };
                }
                return null;
            }
        }


        public Report GetByCardNo(string cardNo)
        {
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                string query = $" select * from report where PatientCardNo = '{cardNo}'";
                var command = new MySqlCommand(query, conn);
                var reportReader = command.ExecuteReader();
                while (reportReader.Read())
                {
                    return new Report
                    {
                        Id = (int)reportReader["Id"],
                        ReportContent = reportReader["ReportContent"].ToString(),
                        IsDeleted = Convert.ToBoolean(reportReader["IsDeleted"]),
                    };
                }
            }
            return null;
        }

        public Report GetAppointmentById(int id)
        {
            using (MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                string query = $" select * from report where AppointmentId = '{id}'";
                var command = new MySqlCommand(query, conn);
                var reportReader = command.ExecuteReader();
                while (reportReader.Read())
                {
                    return new Report
                    {
                        Id = (int)reportReader["Id"],
                        ReportContent = reportReader["ReportContent"].ToString(),
                        PatientComplain = reportReader["PatientComplain"].ToString(),
                        IsDeleted = Convert.ToBoolean(reportReader["IsDeleted"]),
                    };
                }
            }
            return null;
        }

        public List<Report> GetAll()
        {
            using(MySqlConnection conn = new(DentalLabDbContext.connections))
            {
                conn.Open();
                var reportList = new List<Report>();
                string query = $" select * from report";
                var command =new MySqlCommand(query, conn);
                var reportReader = command.ExecuteReader();
                while(reportReader.Read())
                {
                    reportList.Add(new Report
                    {
                        Id = (int)reportReader["Id"],
                        ReportContent = reportReader["ReportContent"].ToString(),
                        IsDeleted = Convert.ToBoolean(reportReader["IsDeleted"])
                    });
                }
                return reportList;
            }
        }
    }
}
