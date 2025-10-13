using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Models
{
    public class ExercisePlanDetail
    {
        public int NumOfSets { get; set; }
        public int NumOfRepeats { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }

        public int ExercisePlanId { get; set; }
        public virtual  ExercisePlan? ExercisePlan { get; set; }

        public int ExerciseId { get; set; }
        public virtual Exercise? Exercise { get; set; }
    
    }
}
