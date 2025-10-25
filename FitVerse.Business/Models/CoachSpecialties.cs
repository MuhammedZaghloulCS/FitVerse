using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Models
{
    public class CoachSpecialties
    {
        public string CoachId { get; set; }
        public virtual Coach? Coach { get; set; }
        public int SpecialtyId { get; set; }
        public virtual Specialty? Specialty { get; set; }
        public string Certification { get; set; } = string.Empty;

    }
}
