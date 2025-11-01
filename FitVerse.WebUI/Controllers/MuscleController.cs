using FitVerse.Core.IService;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.ExerciseVM;
using FitVerse.Core.ViewModels.Meuscle;
using FitVerse.Data.Models;
using FitVerse.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Web.Controllers
{
    public class MuscleController : Controller
    {
        private readonly IMuscleService _muscleService;

        public MuscleController(IMuscleService muscleService)
        {
            _muscleService = muscleService;
        }

      
        public IActionResult Index()
        {
            return View();
        }

 
        [HttpGet]
        public IActionResult GetAll()
        {
            var muscles = _muscleService.GetAllMuscles();
            var data = muscles.Select(m => new { m.Id, m.Name }).ToList();
            return Json(new { data = data }); 
        }


        [HttpGet]
        public IActionResult GetById(int id)
        {
            var muscle = _muscleService.GetMuscleById(id);
            if (muscle == null)
                return Json(new { Success = false, Message = "Muscle not found!" });

            return Json(new { Success = true, Data = muscle });
        }

    
        [HttpPost]
        public IActionResult Create(AddMuscleVM model)
        {
            var result = _muscleService.AddMuscle(model);
            return Json(new { Success = result.Success, Message = result.Message });
        }

        [HttpPost]
        public IActionResult Update(MuscleVM model)
        {
            var result = _muscleService.UpdateMuscle(model);
            return Json(new { Success = result.Success, Message = result.Message });
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _muscleService.DeleteMuscle(id);
            return Json(new { Success = result.Success, Message = result.Message });
        }


        [HttpGet]
        public IActionResult GetPaged(int page = 1, int pageSize = 6, string? search = null, int? anatomyId = null)
        {
            var (data, totalItems) = _muscleService.GetPagedMuscles(page, pageSize, search, anatomyId);
            return Json(new
            {
                data = data,
                currentPage = page,
                totalPages = (int)Math.Ceiling((double)totalItems / pageSize)
            });
        }

   
        [HttpGet]
        public IActionResult GetAnatomyGroups()
        {
            var anatomies = _muscleService.GetAllMuscles()
                .Select(m => new { Id = m.AnatomyId, Name = m.AnatomyName })
                .Distinct()
                .ToList();

            return Json(new { success = true, data = anatomies });
        }

        // ==========================

        [HttpGet]
        public IActionResult GetStats()
        {
            var (totalMuscles, totalAnatomyGroups, totalExercises) = _muscleService.GetStats();
            return Json(new
            {
                TotalMuscles = totalMuscles,
                TotalAnatomyGroups = totalAnatomyGroups,
                TotalExercises = totalExercises
            });
        }
    }
}
