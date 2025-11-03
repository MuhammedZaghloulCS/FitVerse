using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.Admin
{
    public class TopCoachViewModel
    {
        public string CoachId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int ClientsCount { get; set; }
        public double Rating { get; set; }
        public int FeedbackCount { get; set; }
    }
}
