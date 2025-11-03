using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.Profile
{
    public class CoachViewModel
    {
        public int? ExperienceYears { get; set; }
        public string? About { get; set; }

        [Required(ErrorMessage ="Please, Enter the salary")]
        [Range(7000,100000, ErrorMessage = "Please,Adhere to the limit of the salary ")]
        public decimal? Salary { get; set; }
        [Required(ErrorMessage = "Please, Enter at least one certificate")]

        public string? Certificates { get; set; }

        public ICollection<String> Specialities { get; set; } = new HashSet<String>();
    }
}
