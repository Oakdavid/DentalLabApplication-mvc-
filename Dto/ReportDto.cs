using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Dto
{
    public class ReportDto
    {
        public string RefNumber { get; set; }
        public string DrName { get; set; }
        public string PatientCardNo { get; set; }
        public string ReportContent { get; set; }
    }

    public class ReportRequestModelDto()
    {
        public string ReportContent { get; set; }
        public string RefNumber { get; set; }

    }
    public class UpdateRequestModelDto
    {
        public string ReportContent { get; set; }
    }
    public class ReportDeleteModelDto
    {
        public string ReportContent { get; set; }
    }
}
