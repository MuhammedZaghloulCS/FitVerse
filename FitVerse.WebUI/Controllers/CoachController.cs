using AutoMapper;
using FitVerse.Core.IService;
using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Client;
using FitVerse.Core.ViewModels.Coach;
using FitVerse.Core.ViewModels.Equipment;
using FitVerse.Data.Models;
using FitVerse.Data.Service.FitVerse.Data.Service;
using FitVerse.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitVerse.Web.Controllers
{
    public class CoachController : Controller
    {
        private readonly IUnitOFWorkService unitOFWorkService;
        private readonly IMapper mapper;

        //private readonly ICoachService coachService;

        public CoachController(IUnitOFWorkService unitOFWorkService)
        {
            this.unitOFWorkService = unitOFWorkService;

        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Dashboard() {
            string coachId ="1";
            var model = unitOFWorkService.CoachService.GetDashboardData(coachId);
            return View("Dashboard",model);
        }
        public IActionResult ClientCoaches()
        {
            return View("ClientCoaches");
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
    

            var coach = unitOFWorkService.CoachService.GetCoachByIdGuid(Id);
      
           
            if (coach == null)
                return Json(new { success = false, message = "Coach not found." });

            return Json(new { success = true, data = coach });
        }


        [HttpDelete]
        public IActionResult DeleteCoach(string Id)
        {

            var coach = unitOFWorkService.CoachService.DeleteCoachById(Id);
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
        [HttpGet]
        public IActionResult GetAllClients()
        {
            //var coachId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string coachId = "1";// Coach logged in ID

            var clients = unitOFWorkService.CoachRepository.GetAllClientsByCoach(coachId);

            return Json(new { data = clients });
        }
        [HttpGet]
        public IActionResult GetPackagesByCoachId(string coachId)
        {
            var packages = unitOFWorkService.CoachService.GetPackagesByCoachId(coachId);
            return Json(new { data = packages });
        }


      

    }


        }

}

