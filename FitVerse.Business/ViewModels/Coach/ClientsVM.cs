using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.Coach
{
    public class ClientsVM
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public double Height { get; set; }
        public double StartWeight { get; set; }
        public string Goal { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Image { get; set; } = null!;
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }

      
        public int TotalWorkouts { get; set; }       
        public double ProgressPercentage { get; set; } 
        public string SubscriptionName { get; set; } = string.Empty;
    }
}
