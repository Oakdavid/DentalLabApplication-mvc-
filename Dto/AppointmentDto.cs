using DentalLabConsoleApplicationWithAdo.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Dto
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string RefNumber { get; set; }
        public string CardNo { get; set; }
        public string DrNumber { get; set; }
        public string PatientComplain { get; set; }
        public DateTime DateOfAppointment { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public string ReportContent { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public string Location { get; set; }
    }

    public class AppointmentRequestModel
    {
        public string RefNumber { get; set; }         // is this right?
        public string CardNo { get; set; }
        public string DrNumber { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId {  get; set; }
        public DateTime DateOfAppointment { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public string PatientComplain { get; set; }
    }
}
