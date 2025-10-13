using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Models
{
    public class DietPlan
    {
        public int Id { get; set; }
        public double TotalCal { get; set; }
        public double ProteinInGrams { get; set; }
        public double CarbInGrams { get; set; }
        public double FatsInGrams { get; set; }


        public Guid ClientId { get; set; }
        public virtual Client? Client { get; set; }


        public Guid CoachId { get; set; }
        public virtual Coach? Coach { get; set; }

    }

}
