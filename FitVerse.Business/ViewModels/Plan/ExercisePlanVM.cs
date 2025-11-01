using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.Plan
{
    public class ExercisePlanVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DurationWeeks { get; set; }
        //public string Difficulty { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }

        public string? ClientId { get; set; }
        public string? CoachId { get; set; }

        public int clientCount { get; set; }

        public List<int>? SelectedExerciseIds { get; set; }

        public int DefaultSets { get; set; } = 3;
        public int DefaultRepeats { get; set; } = 10;

        public List<ExercisePlanDetailVM>? ExercisePlanDetails { get; set; }

    }
}
