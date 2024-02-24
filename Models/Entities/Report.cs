using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Models.Entities
{
    public class Report : BaseEntity
    {
        public string RefNumber { get; set; }
        public string DrName { get; set; }
        public string PatientCardNo { get; set; }
        public string ReportContent { get; set; }

    }
}
