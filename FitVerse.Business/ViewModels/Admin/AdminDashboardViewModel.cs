using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitVerse.Data.Models;

namespace FitVerse.Core.ViewModels.Admin
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalCoaches { get; set; }
        public int SoldedPackages { get; set; }
        public IEnumerable<TopCoachViewModel> TopCoaches { get; set; }
    }
}
