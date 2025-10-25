using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.Meuscle
{
    public class AddMuscleVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AnatomyId { get; set; }
        public int ExerciseCount { get; set; }
    }

}
