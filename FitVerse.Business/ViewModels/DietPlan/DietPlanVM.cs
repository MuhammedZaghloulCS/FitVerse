using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.DietPlan
{
    public class DietPlanVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; } = "Male";
        public double Weight { get; set; }
        public double Height { get; set; }
        public double TotalCal { get; set; }
        public double ProteinInGrams { get; set; }
        public double CarbInGrams { get; set; }
        public double FatsInGrams { get; set; }
        public string Goal { get; set; } = string.Empty;
        public string ClientId { get; set; }
        public string CoachId { get; set; } // ✅ إضافة CoachId
        public double ActivityMultiplier { get; set; } = 1.2;
        public double ProteinPercentage { get; set; }
        public double CarbPercentage { get; set; }
        public double FatPercentage { get; set; }
        public string Notes { get; set; } = string.Empty; // ✅ إضافة Notes

    }
}
