using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.ViewModels.ClientDashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FitVerse.Web.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientDashboardController : Controller
    {
        private readonly IUnitOFWorkService _unitOfWorkService;
        private readonly ILogger<ClientDashboardController> _logger;

        public ClientDashboardController(IUnitOFWorkService unitOfWorkService, ILogger<ClientDashboardController> logger)
        {
            _unitOfWorkService = unitOfWorkService;
            _logger = logger;
        }

        public IActionResult Dashboard()
        {
            try
            {
                _logger.LogInformation("Loading client dashboard");
                
                var model = _unitOfWorkService.ClientDashboardService.GetClientDashboard();
                
                if (model == null)
                {
                    _logger.LogWarning("Client dashboard model is null - redirecting to login");
                    return RedirectToAction("Login", "Account");
                }

                // Always show dashboard, whether client has coach or not
                if (model.HasActiveSubscription)
                {
                    _logger.LogInformation($"Successfully loaded dashboard for client {model.ClientId} with coach {model.CoachName}");
                }
                else
                {
                    _logger.LogInformation($"Successfully loaded dashboard for client {model.ClientId} without coach - showing coach selection");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading client dashboard");
                TempData["Error"] = "An error occurred while loading your dashboard. Please try again.";
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
