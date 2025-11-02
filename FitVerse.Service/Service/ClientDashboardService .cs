using FitVerse.Core.IService;
using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.ClientDashboard;
using FitVerse.Data.Models;
using FitVerse.Data.Service.FitVerse.Data.Service;
using FitVerse.Service.Service;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitVerse.Core.UnitOfWorkServices
{
    public class ClientDashboardService : IClientDashboardService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IClientService clientService;
        private readonly ICoachService coachService;
        private readonly IImageHandleService imageService;
        private readonly IHttpContextAccessor httpContext;

        public ClientDashboardService(IUnitOfWork unitOfWork , IClientService clientService, ICoachService coachService, IImageHandleService imageService,IHttpContextAccessor httpContext)
        {
            this.unitOfWork = unitOfWork;
            this.clientService = clientService;
            this.coachService = coachService;
            this.imageService = imageService;
            this.httpContext = httpContext;
        }

        public ClientDashboardViewModel GetClientDashboard()
        {

            var clientId = httpContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var client = clientService.GetById(clientId);
            
            if (client == null) return null;

            var coach = coachService.GetCoachByClientId(clientId);
            //if (coach == null) return null;

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
                ClientName = client.UserId == null ? "UnKnown" : client?.User?.FullName,
                ClientId = client?.Id,
                CoachName = coach == null ? null : coach?.User?.FullName,
                //CoachId = coach?.Id,
                CoachExperience =coach==null?0: (int)coach?.ExperienceYears,
                ExercisePlanSummary =exercisePlan==null?"--": exercisePlan?.Notes,
                DietPlanSummary =dietPlan==null?"--": dietPlan?.Notes,
                //Specialists = unitOfWork.Coaches.GetCoachspecialtiesByCoachId(coach?.Id),
                CoachImagePath = coach == null ? "--" : coach?.User?.ImagePath,
                ClientImagePath = client.UserId == null ? "--" : client?.User?.ImagePath
            };

            return model;
        }
    }
}
