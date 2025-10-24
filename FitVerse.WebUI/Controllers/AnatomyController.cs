using AutoMapper;
using FitVerse.Core.IService;
using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Anatomy;
using FitVerse.Data.Models;
using FitVerse.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class AnatomyController : Controller
    {
        private readonly IUnitOFWorkService unitOFWorkService;
        private readonly IMapper mapper;



        public AnatomyController(IUnitOFWorkService unitOFWorkService, IMapper mapper)
        {
            this.unitOFWorkService = unitOFWorkService;
            this.mapper = mapper;
            
        }
        public IActionResult Index()
        {
            var model = new AnatomyDashboardVM
            {
                AnatomyCount = unitOFWorkService.AnatomyService.GetAllCount(),
                MuscleCount = unitOFWorkService.AnatomyService.GetMuscleCount(),
                ExerciseCount = unitOFWorkService.AnatomyService.GetExerciseCount()
            };

            return View(model);
           
        }

        public IActionResult GetAll()
        {

            var allObj = unitOFWorkService.AnatomyService.GetAll();
            return Json(new { data = allObj });
        }


        public IActionResult GetById(int id)
        {
            var anatomy = unitOFWorkService.AnatomyService.GetById(id);
            if (anatomy == null)
            {
                return Json(new { success = false, message = "Somthing wrong!" });
            }
   
            return Json(new { success = true, data = anatomy });

        }
        public IActionResult Create(AddAnatomyVM model)
        {
            var anatomy = unitOFWorkService.AnatomyService.Create(model);
            return Json(new
            {
                success = anatomy,
                message = anatomy ? "Anatomy created successfully" : "Something went wrong!"
            });
        }
        public IActionResult Delete(int id)
        {
            var anatomy = unitOFWorkService.AnatomyService.Delete(id);

            return Json(new
            {
                success = anatomy,
                message = anatomy ? "Anatomy Deleted successfully" : "Something went wrong!"
            });
        }
        public IActionResult Update(AnatomyVM model)
        {
            bool result = unitOFWorkService.AnatomyService.Update(model);
            return Json(new
            {
                success = result,
                message = result ? "Anatomy updated successfully" : "Something went wrong!"
            });
        }

        public IActionResult GetPaged(int page = 1, int pageSize = 5, string? search = null)
        {
            var result = unitOFWorkService.AnatomyService.GetPaged(page, pageSize, search);
            return Json(new
            {
                data = result.Data,
                currentPage = result.CurrentPage,
                totalPages = result.TotalPages
            });
        }
        public IActionResult GetMuscleCountByAnatomy(int anatomyId)
        {
            var count = unitOFWorkService.AnatomyService.GetCountByAnatomy(anatomyId);
            return Json(new { count });

        }
        public IActionResult GetMusclesByAnatomyId(int anatomyId)
        {
            var muscles = unitOFWorkService.AnatomyService.GetMusclesByAnatomyId(anatomyId);

            var data = muscles.Select(m => new
            {
                m.Id,
                m.Name,
            }).ToList();

            return Json(new { data });
        }




    }
}
