using AutoMapper;
using FitVerse.Core.IService;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Coach;
using FitVerse.Data.Models;
using FitVerse.Data.Service.FitVerse.Data.Service;
using Microsoft.AspNetCore.Mvc;
using FitVerse.Core.IService;
using FitVerse.Data.Service;

namespace FitVerse.Web.Controllers
{
    public class CoachController : Controller
    {

        private readonly ICoachService coachService;

        public CoachController(ICoachService coachService)
        {
            this.coachService = coachService;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCoach([FromForm] AddCoachVM model)
        {
            var result = coachService.AddCoach(model);

            if (result.Success)
                return Json(new { success = true, message = result.Message });
            else
                return Json(new { success = false, message = result.Message });
        }

    }

}

