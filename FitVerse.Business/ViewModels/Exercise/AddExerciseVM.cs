using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.Exercise
{
    public class AddExerciseVM
    {
        public string Name { get; set; }
        public string? VideoLink { get; set; }
        public string Description { get; set; }

        public  Muscle? Muscle { get; set; }

        public FitVerse.Data.Models.Equipment Equipment { get; set; }
    }
}
