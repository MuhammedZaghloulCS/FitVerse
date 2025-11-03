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
using System;
using System.Collections.Generic;
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

                // Get client with all related data using GetAll with includeProperties
                var client = unitOfWork.Clients
                    .GetAll(filter: c => c.Id == clientId, 
                           includeProperties: "User,ClientSubscriptions,ClientSubscriptions.Coach,ClientSubscriptions.Coach.User,ClientSubscriptions.Package")
                    .FirstOrDefault();

                if (client == null)
                {
                    logger.LogWarning($"Client not found for ID: {clientId}");
                    return null;
                }

                // Check if client has an active subscription with a coach
                var activeSubscription = client.ClientSubscriptions
                    .FirstOrDefault(cs => cs.Status == "Active" && cs.EndDate >= DateTime.Now);

                if (activeSubscription == null || activeSubscription.Coach == null)
                {
                    logger.LogInformation($"Client {clientId} has no active coach subscription - showing dashboard with coach selection");
                    
                    // Get available coaches for selection
                    var availableCoaches = unitOfWork.Coaches
                        .GetAll(filter: c => c.User != null && c.User.Status == "Active", 
                               includeProperties: "User")
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

                // We already have the coach from the included subscription
                var coach = activeSubscription.Coach;

                if (coach == null)
                {
                    logger.LogWarning($"Coach not found for ID: {activeSubscription.CoachId}");
                    return null;
                }

                // Get latest plans with optimized queries using GetQueryable for database-level filtering
                var exercisePlan = unitOfWork.ExercisePlans
                    .GetQueryable()
                    .Where(e => e.ClientId == clientId && e.CoachId == coach.Id)
                    .OrderByDescending(e => e.Id)
                    .FirstOrDefault();

                var dietPlan = unitOfWork.DietPlans
                    .GetQueryable()
                    .Where(d => d.ClientId == clientId && d.CoachId == coach.Id)
                    .OrderByDescending(d => d.Id)
                    .FirstOrDefault();
                
                // Log for debugging
                logger.LogInformation($"Exercise Plan found: {exercisePlan != null}, Diet Plan found: {dietPlan != null} for Client: {clientId}, Coach: {coach.Id}");

                // Prepare optimized ViewModel
                var model = new ClientDashboardViewModel
                {
                    ClientName = client.User?.FullName ?? "Unknown",
                    ClientId = client.Id,
                    CoachName = coach.User?.FullName,
                    CoachId = coach.Id,
                    CoachExperience = coach.ExperienceYears ?? 0,
                    ExercisePlanSummary = exercisePlan?.Notes ?? "--",
                    DietPlanSummary = dietPlan != null 
                        ? (!string.IsNullOrWhiteSpace(dietPlan.Notes) 
                            ? dietPlan.Notes 
                            : $"{dietPlan.Name} - {dietPlan.TotalCal:F0} cal/day - Goal: {dietPlan.Goal}")
                        : "--",
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
