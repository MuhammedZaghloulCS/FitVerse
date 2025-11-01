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

        public string ClientId { get; set; }
        public virtual Client? Client { get; set; }

        public string CoachId { get; set; }
        public virtual Coach? Coach { get; set; }

        // نشاط العميل: يمكن قيم مثل 1.2, 1.375, 1.55, 1.725, 1.9
        public double ActivityMultiplier { get; set; } = 1.2;
        public string Goal { get; set; } = "Maintain";
        public string Notes { get; set; } = string.Empty;

    }


}
