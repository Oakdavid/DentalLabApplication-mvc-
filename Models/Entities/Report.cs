using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Models.Entities
{
    public class Report : BaseEntity
    {
        public Appointment Appointment { get; set; }
        public int? AppointmentId { get; set; }
        public string? ReportContent { get; set; }
        public string PatientComplain { get; set; }         // just added

    }
}
