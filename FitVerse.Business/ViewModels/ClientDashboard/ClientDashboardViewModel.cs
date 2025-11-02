

namespace FitVerse.Core.ViewModels.ClientDashboard
{
    public class ClientDashboardViewModel
    {
        public string ClientName { get; set; }
        public string ClientId { get; set; }    
        public string CoachName { get; set; }
        public string CoachId { get; set; }
        public int CoachExperience { get; set; }
        public List<FitVerse.Data.Models.Specialty> Specialists { get; set; }
        public string ExercisePlanSummary { get; set; }
        public string DietPlanSummary { get; set; }
        public string CoachImagePath {get;set; }
        public string ClientImagePath { get; set; }
    }
}
