using FitVerse.Core.IService;
using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.ClientDashboard;
using FitVerse.Data.Models;
using FitVerse.Data.Service.FitVerse.Data.Service;
using FitVerse.Service.Service;
using System.Linq;
using System.Threading.Tasks;

namespace FitVerse.Core.UnitOfWorkServices
{
    public class ClientDashboardService : IClientDashboardService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IClientService clientService;
        private readonly ICoachService coachService;
        private readonly IImageHandleService imageService;


        public ClientDashboardService(IUnitOfWork unitOfWork , IClientService clientService, ICoachService coachService, IImageHandleService imageService)
        {
            this.unitOfWork = unitOfWork;
            this.clientService = clientService;
            this.coachService = coachService;
            this.imageService = imageService;
        }

        public ClientDashboardViewModel GetClientDashboard(string clientId)
        {
            var client = clientService.GetById(clientId);
            if (client == null) return null;

            var coach = coachService.GetCoachByClientId(clientId);
            if (coach == null) return null;

            var exercisePlan = unitOfWork.ExercisePlans
                .GetAll()
                .Where(e => e.ClientId == clientId && e.CoachId == coach.Id)
                .LastOrDefault();

            var dietPlan = unitOfWork.DietPlans
                .GetAll()
                .Where(e => e.ClientId == clientId && e.CoachId == coach.Id)
                .LastOrDefault();

            // تحضير الـ ViewModel
            var model = new ClientDashboardViewModel
            {
                ClientName = client.Name,
                ClientId = client.Id,
                CoachName = coach.Name,
                CoachId = coach.Id,
                CoachExperience = coach.ExperienceYears,
                ExercisePlanSummary = exercisePlan?.Notes,
                DietPlanSummary = dietPlan?.Notes,
                Specialists = unitOfWork.Coaches.GetCoachspecialtiesByCoachId(coach.Id),
                CoachImagePath = coach.ImagePath,
                ClientImagePath = client.Image
            };

            return model;
        }
    }
}
