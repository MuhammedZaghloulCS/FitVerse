using FitVerse.Core.IService;
using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.ClientDashboard;
using FitVerse.Data.Models;
using FitVerse.Data.Service.FitVerse.Data.Service;
using FitVerse.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
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
        private readonly ILogger<ClientDashboardService> logger;

        public ClientDashboardService(IUnitOfWork unitOfWork, IClientService clientService, ICoachService coachService, IImageHandleService imageService, IHttpContextAccessor httpContext, ILogger<ClientDashboardService> logger)
        {
            this.unitOfWork = unitOfWork;
            this.clientService = clientService;
            this.coachService = coachService;
            this.imageService = imageService;
            this.httpContext = httpContext;
            this.logger = logger;
        }

        public ClientDashboardViewModel GetClientDashboard()
        {
            try
            {
                // Get current user ID efficiently
                var clientId = httpContext.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                
                if (string.IsNullOrEmpty(clientId))
                {
                    logger.LogWarning("No client ID found in current user claims");
                    return null;
                }

                // Single query to get client with user data
                var client = unitOfWork.Clients
                    .Find(c => c.Id == clientId)
                    .FirstOrDefault();

                if (client == null)
                {
                    logger.LogWarning($"Client not found for ID: {clientId}");
                    return null;
                }

                // Check if client has a coach
                var coachSubscription = client.ClientSubscriptions.FirstOrDefault();
                if (coachSubscription == null)
                {
                    logger.LogInformation($"Client {clientId} has no coach assigned - showing dashboard with coach selection");
                    
                    // Get available coaches for selection
                    var availableCoaches = unitOfWork.Coaches
                        .Find(c => c.User.Status == "Active")
                        .Take(6) // Limit to 6 coaches for UI
                        .ToList();

                    return new ClientDashboardViewModel
                    {
                        ClientName = client.User?.FullName ?? "Unknown",
                        ClientId = client.Id,
                        ClientImagePath = client.User?.ImagePath ?? "--",
                        CoachName = null,
                        CoachExperience = 0,
                        ExercisePlanSummary = "No exercise plan yet",
                        DietPlanSummary = "No diet plan yet",
                        CoachImagePath = "--",
                        HasActiveSubscription = false,
                        AvailableCoaches = availableCoaches
                    };
                }

                // Get coach with user data in a single query
                var coach = unitOfWork.Coaches
                    .Find(c => c.Id == coachSubscription.CoachId)
                    .FirstOrDefault();

                if (coach == null)
                {
                    logger.LogWarning($"Coach not found for ID: {coachSubscription.CoachId}");
                    return null;
                }

                // Get latest plans with optimized queries
                var exercisePlan = unitOfWork.ExercisePlans
                    .GetAll()
                    .Where(e => e.ClientId == clientId && e.CoachId == coach.Id)
                    .OrderByDescending(e => e.Id)
                    .FirstOrDefault();

                var dietPlan = unitOfWork.DietPlans
                    .GetAll()
                    .Where(d => d.ClientId == clientId && d.CoachId == coach.Id)
                    .OrderByDescending(d => d.Id)
                    .FirstOrDefault();

                // Prepare optimized ViewModel
                var model = new ClientDashboardViewModel
                {
                    ClientName = client.User?.FullName ?? "Unknown",
                    ClientId = client.Id,
                    CoachName = coach.User?.FullName,
                    CoachExperience = (int)coach.ExperienceYears,
                    ExercisePlanSummary = exercisePlan?.Notes ?? "--",
                    DietPlanSummary = dietPlan?.Notes ?? "--",
                    CoachImagePath = coach.User?.ImagePath ?? "--",
                    ClientImagePath = client.User?.ImagePath ?? "--",
                    HasActiveSubscription = true,
                    AvailableCoaches = new List<Coach>() // Empty list when subscribed
                };

                logger.LogInformation($"Successfully loaded dashboard for client {clientId}");
                return model;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading client dashboard");
                return null;
            }
        }
    }
}
