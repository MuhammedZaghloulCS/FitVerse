using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.Plan
{
    public class ExercisePlanDetailVM
    {
        public int ExerciseId { get; set; }
        public string? ExerciseName { get; set; }
        public int NumOfSets { get; set; }
        public int NumOfRepeats { get; set; }
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }
    }
}
