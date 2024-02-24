using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Models.Entities
{
    public class Patient : BaseEntity
    {
        public int ProfileId { get; set; }
        public string CardNo { get; set; }

    }
}
