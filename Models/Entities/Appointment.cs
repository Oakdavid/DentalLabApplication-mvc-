using DentalLabConsoleApplicationWithAdo.Dto;
using DentalLabConsoleApplicationWithAdo.Models.Enum;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Models.Entities
{
    public class Appointment : BaseEntity
    {
        public string RefNumber { get; set; }
        public string CardNo { get; set; }
        public string DrNumber { get; set; }
        public string PatientComplain { get; set; }
        public DateTime DateOfAppointment { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }  // doctor will set the date
        public string ReportContent { get; set; }
        public AppointmentType AppointmentType { get; set; }          // appointment type

        public string Location { get; set; }
    }
}       
