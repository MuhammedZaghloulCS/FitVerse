using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.ViewModels.DailyLog;
using FitVerse.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FitVerse.Web.Controllers
{
    public class DailyLogController : Controller
    {
        private readonly IUnitOFWorkService unitOFWorkService;

        public DailyLogController(IUnitOFWorkService unitOFWorkService)
        {
            this.unitOFWorkService = unitOFWorkService;
        }

        public IActionResult ClientLogs(string clientId = "1")
        {
            var logs = unitOFWorkService.DailyLogService.GetClientLogs(clientId);
            return View(logs);
        }

        [HttpPost]
        public IActionResult AddClientLog(ClientAddDailyLogInputVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Please check the entered data ";
                    var logs = unitOFWorkService.DailyLogService.GetClientLogs("1");
                    return View("ClientLogs", logs);
                }

                var log = new DailyLog
                {
                    ClientId = "1",
                    CoachId = "C1",
                    CurrentWeight = model.CurrentWeight,
                    ClientNotes = model.ClientNotes,
                    LogDate = DateTime.UtcNow,
                    PhotoPath = unitOFWorkService.ImageHandleService.SaveImage(model.Photo),
                    IsReviewed = false
                };

                unitOFWorkService.DailyLogService.AddClientLog(log);
                TempData["SuccessMessage"] = "Daily log saved successfully ";

                return RedirectToAction("ClientLogs", new { clientId = log.ClientId });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error while saving ❌: {ex.Message}";
                var logs = unitOFWorkService.DailyLogService.GetClientLogs("1");
                return View("ClientLogs", logs);
            }
        }

        public IActionResult CoachLogs(string coachId = "c1")
        {
            var logs = unitOFWorkService.DailyLogService.GetCoachLogs(coachId);
            return View(logs);
        }

        [HttpPost]
        public IActionResult ReviewLog(int id, string feedback, int rating)
        {
            unitOFWorkService.DailyLogService.CoachReviewLog(id, feedback, rating);
            return RedirectToAction("CoachLogs", new { coachId = "c1" });
        }
    }
}
