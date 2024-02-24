using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalLabConsoleApplicationWithAdo.Models.Entities
{
    public class Doctor : BaseEntity
    {
        public int Id { get; set; }
        public int ProfileId {  get; set; }
        public string LicenseNumber { get; set; }
        public string Education { get; set; }
        public int YearsOfExperience { get; set; }
        public string Specializations { get; set; }
        public string SpecializationDescription { get; set; }

        public override string ToString()
        {
            return $"{Specializations},{SpecializationDescription}";
        }

    }
}
