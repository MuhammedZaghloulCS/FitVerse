using FitVerse.Core.ViewModels.Client;
using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.Coach
{
    public class CoachDashboardViewModel
    {
            public int ActiveClientsCount { get; set; }
            public int TotalPlans { get; set; }
            public int TotalExercises { get; set; }
            public double AverageRating { get; set; }
           public List<ClientDashVM> RecentClients { get; set; }

    }
}
