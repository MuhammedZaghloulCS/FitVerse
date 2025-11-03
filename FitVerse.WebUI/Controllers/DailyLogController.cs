using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.ViewModels.DailyLog;
using FitVerse.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FitVerse.Web.Controllers
{
    public class DailyLogController : Controller
    {
        private readonly IUnitOFWorkService unitOFWorkService;
        private readonly ILogger<DailyLogController> _logger;

        public DailyLogController(IUnitOFWorkService unitOFWorkService, ILogger<DailyLogController> logger)
        {
            this.unitOFWorkService = unitOFWorkService;
            this._logger = logger;
        }

        [Authorize(Roles = "Client")]
        public IActionResult ClientLogs()
        {
            try
            {
                // Get current authenticated client ID
                var currentClientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                if (string.IsNullOrEmpty(currentClientId))
                {
                    _logger.LogWarning("No client ID found in current user claims");
                    return RedirectToAction("Login", "Account");
                }

                _logger.LogInformation($"Loading daily logs for client: {currentClientId}");
                
                var logs = unitOFWorkService.DailyLogService.GetClientLogs(currentClientId);
                
                _logger.LogInformation($"Found {logs?.Count() ?? 0} logs for client {currentClientId}");
                
                return View(logs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading client daily logs");
                TempData["ErrorMessage"] = "An error occurred while loading your daily logs. Please try again.";
                return View(new List<DailyLog>());
            }
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        public IActionResult AddClientLog(ClientAddDailyLogInputVM model)
        {
            try
            {
                // Get current authenticated client ID
                var currentClientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                if (string.IsNullOrEmpty(currentClientId))
                {
                    _logger.LogWarning("No client ID found in current user claims during log creation");
                    return RedirectToAction("Login", "Account");
                }

                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Please check the entered data and try again.";
                    var logs = unitOFWorkService.DailyLogService.GetClientLogs(currentClientId);
                    return View("ClientLogs", logs);
                }

                // Get client's current coach (if any)
                var client = unitOFWorkService.ClientRepository
                    .Find(c => c.UserId == currentClientId)
                    .FirstOrDefault();
                
                // Get the coach ID from the client's active subscription
                var coachId = client?.ClientSubscriptions?.FirstOrDefault()?.CoachId; // This might be null if client doesn't have a coach yet

                var log = new DailyLog
                {
                    ClientId = currentClientId,
                    CoachId = coachId, // Will be null if no coach assigned
                    CurrentWeight = model.CurrentWeight,
                    ClientNotes = model.ClientNotes,
                    LogDate = DateTime.UtcNow,
                    PhotoPath = unitOFWorkService.ImageHandleService.SaveImage(model.Photo),
                    IsReviewed = false
                };

                unitOFWorkService.DailyLogService.AddClientLog(log);
                
                _logger.LogInformation($"Daily log created successfully for client {currentClientId}");
                TempData["SuccessMessage"] = "Daily log saved successfully!";

                return RedirectToAction("ClientLogs");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while saving daily log for client {User.FindFirstValue(ClaimTypes.NameIdentifier)}");
                TempData["ErrorMessage"] = $"Error while saving: {ex.Message}";
                
                var currentClientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrEmpty(currentClientId))
                {
                    var logs = unitOFWorkService.DailyLogService.GetClientLogs(currentClientId);
                    return View("ClientLogs", logs);
                }
                
                return View("ClientLogs", new List<DailyLog>());
            }
        }

        [Authorize(Roles = "Coach")]
        public IActionResult CoachLogs()
        {
            try
            {
                // Get current authenticated coach ID
                var currentCoachId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                if (string.IsNullOrEmpty(currentCoachId))
                {
                    _logger.LogWarning("No coach ID found in current user claims");
                    return RedirectToAction("Login", "Account");
                }

                _logger.LogInformation($"Loading daily logs for coach: {currentCoachId}");
                
                var logs = unitOFWorkService.DailyLogService.GetCoachLogs(currentCoachId);
                
                _logger.LogInformation($"Found {logs?.Count() ?? 0} logs for coach {currentCoachId}");
                
                return View(logs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading coach daily logs");
                TempData["ErrorMessage"] = "An error occurred while loading daily logs. Please try again.";
                return View(new List<DailyLog>());
            }
        }

        [HttpPost]
        [Authorize(Roles = "Coach")]
        public IActionResult ReviewLog(int id, string feedback, int rating)
        {
            try
            {
                // Get current authenticated coach ID
                var currentCoachId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                if (string.IsNullOrEmpty(currentCoachId))
                {
                    _logger.LogWarning("No coach ID found in current user claims during log review");
                    return RedirectToAction("Login", "Account");
                }

                // Validate input
                if (id <= 0 || string.IsNullOrWhiteSpace(feedback) || rating < 1 || rating > 5)
                {
                    TempData["ErrorMessage"] = "Please provide valid feedback and rating (1-5 stars).";
                    return RedirectToAction("CoachLogs");
                }

                _logger.LogInformation($"Coach {currentCoachId} reviewing log {id} with rating {rating}");
                
                unitOFWorkService.DailyLogService.CoachReviewLog(id, feedback, rating);
                
                TempData["SuccessMessage"] = "Log review submitted successfully!";
                
                return RedirectToAction("CoachLogs");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while reviewing log {id} by coach {User.FindFirstValue(ClaimTypes.NameIdentifier)}");
                TempData["ErrorMessage"] = $"Error while submitting review: {ex.Message}";
                return RedirectToAction("CoachLogs");
            }
        }
    }
}
