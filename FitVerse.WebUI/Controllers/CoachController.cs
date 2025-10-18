using AutoMapper;
using FitVerse.Core.IService;
using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Coach;
using FitVerse.Core.ViewModels.Equipment;
using FitVerse.Data.Models;
using FitVerse.Data.Service.FitVerse.Data.Service;
using FitVerse.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class CoachController : Controller
    {
        private readonly IUnitOFWorkService unitOFWorkService;

        //private readonly ICoachService coachService;

        public CoachController(IUnitOFWorkService unitOFWorkService)
        {
            this.unitOFWorkService = unitOFWorkService;

        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult GetAllCoaches()
        {
            var coaches = unitOFWorkService.CoachService.GetAllCoaches();
            if (coaches == null || !coaches.Any())
            {
                return Json(new { success = false, message = "No coaches found." });
            }
            return Json(new { success = true, data = coaches });
        }

        [HttpPost]
        public IActionResult AddCoach([FromForm] AddCoachVM model)
        {
            var result = unitOFWorkService.CoachService.AddCoach(model);

            if (result.Success)
                return Json(new { success = true, message = result.Message });
            else
                return Json(new { success = false, message = result.Message });
        }
        [HttpGet]
        public IActionResult GetCoachById(string Id)
        {
            if (!Guid.TryParse(Id, out Guid guidId))
                return Json(new { success = false, message = "Invalid GUID format." });

            var coach = unitOFWorkService.CoachService.GetCoachByIdGuid(guidId);
            if (coach == null)
                return Json(new { success = false, message = "Coach not found." });

            return Json(new { success = true, data = coach });
        }


        [HttpDelete]
        public IActionResult DeleteCoach(string Id)
        {
            if (!Guid.TryParse(Id, out Guid guidId))
                return Json(new { success = false, message = "Invalid GUID format." });
            var coach = unitOFWorkService.CoachService.DeleteCoachById(guidId);
            if (coach.Success)
                return Json(new { success = true, message = coach.Message });
            else
                return Json(new { success = false, message = coach.Message });
        }


        [HttpPost]
        public IActionResult UpdateCoach([FromForm] AddCoachVM model)
        {
            var result = unitOFWorkService.CoachService.UpdateCoach(model);
            if (result.Success)
                return Json(new { success = true, message = result.Message });
            else
                return Json(new { success = false, message = result.Message });
        }



        [HttpGet]
        public IActionResult GetPaged(int page = 1, int pageSize = 5, string? search = null)
        {
            var (data, totalItems) = unitOFWorkService.CoachService.GetPagedEquipments(page, pageSize, search);

            return Json(new
            {
                data = data,
                totalPages = (int)Math.Ceiling((double)totalItems / pageSize)
            });
        }

    }

}

