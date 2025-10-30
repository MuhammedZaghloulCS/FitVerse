using AutoMapper;
using FitVerse.Core.IService;
using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.viewModels;
using FitVerse.Core.ViewModels.Anatomy;
using FitVerse.Data.Models;
using FitVerse.Data.Service;
using FitVerse.Data.UnitOfWork;
using FitVerse.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class AnatomyController : Controller
    {
        private readonly IUnitOFWorkService unitOFWorkService;
        private readonly IMapper mapper;
        private readonly IImageHandleService imageHandleService;


        public AnatomyController(IUnitOFWorkService unitOFWorkService, IMapper mapper, IImageHandleService imageHandleService)
        {
            this.unitOFWorkService = unitOFWorkService;
            this.mapper = mapper;
            this.imageHandleService = imageHandleService;
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

        public IActionResult GetAll(string? search)
        {

            var allObj = unitOFWorkService.AnatomyService.GetAll(search);
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
       
        [HttpPost]
        public IActionResult AddAnatomy([FromForm] AddAnatomyVM model)
        {
            var result =unitOFWorkService.AnatomyService.AddAnatomy(model);

            return Json(new
            {
                success = result.Success,
                message = result.Message,
                data = result.Success ? model : null
            });

        }


        public IActionResult Delete(int id)
        {
            var result = unitOFWorkService.AnatomyService.Delete(id);

            return Json(new { success = result.Success, message = result.Message });

        }
        [HttpPost]
        public IActionResult Update([FromForm] AddAnatomyVM model)
        {
            var result = unitOFWorkService.AnatomyService.Update(model);
            return Json(new { success = result.Success, message = result.Message });
        }








    }
}
