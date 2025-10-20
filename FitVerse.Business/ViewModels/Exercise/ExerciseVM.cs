using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.ExerciseVM
{
    public class ExerciseVM:AddExerciseVM
    {
        public int Id { get; set; }
        public string MuscleName { get; set; }
        public string EquipmentName { get; set; }
    }
}
