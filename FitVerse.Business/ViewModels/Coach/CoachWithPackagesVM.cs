using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.Coach
{
    public class CoachWithPackagesVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int ExperienceYears { get; set; }

        public List<PackagesVM> Packages { get; set; } = new();
    }
}
