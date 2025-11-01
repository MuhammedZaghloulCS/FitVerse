using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.ClientDashboard
{
    public class ExerciseToDashboardVm
    {
        public string ExerciseName { get; set; }
        public string muscleName{ get; set; }
        public string Url { get; set; }
        public string ExerciseDescription { get; set; }
        public int sets { get; set; }
        public int reps { get; set; }
    }

}
