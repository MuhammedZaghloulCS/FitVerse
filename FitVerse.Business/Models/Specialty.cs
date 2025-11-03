using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Models
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }  
        //public string? Icon { get; set; }
        public string? Image { get; set; }
        //public string Color { get; set; } = "#007bff";

        public virtual ICollection<CoachSpecialties> CoachSpecialties { get; set; }=new HashSet<CoachSpecialties>();
    }
}
