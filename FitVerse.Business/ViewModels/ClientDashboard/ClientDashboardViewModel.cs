using System.Collections.Generic;
using System;
using FitVerse.Data.Models;

namespace FitVerse.Core.ViewModels.ClientDashboard
{
    public class ClientDashboardViewModel
    {
        public string ClientName { get; set; }
        public string ClientId { get; set; }    
        public string CoachName { get; set; }
        public string CoachId { get; set; }
        public int CoachExperience { get; set; }
        public List<Specialty>Specialists { get; set; }
        public string ExercisePlanSummary { get; set; }
        public string DietPlanSummary { get; set; }
        public string CoachImagePath {get;set; }
        public string ClientImagePath { get; set; }
    }
}
