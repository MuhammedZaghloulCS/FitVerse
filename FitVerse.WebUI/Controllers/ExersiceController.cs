
using AutoMapper;
using FitVerse.Core.IService;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.viewModels;
using FitVerse.Core.ViewModels.ExerciseVM;
using FitVerse.Core.ViewModels.Meuscle;
using FitVerse.Data.Models;
using FitVerse.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace FitVerse.Web.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _exerciseService.GetAllExercises();
            return Json(new { success = true, data });
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var data = _exerciseService.GetExerciseById(id);
            if (data == null)
                return Json(new { success = false, message = "Exercise not found!" });

            return Json(new { success = true, data });
        }

        [HttpPost]
        public IActionResult Create(AddExerciseVM model)
        {
            var result = _exerciseService.AddExercise(model);
            return Json(new { success = result.Success, message = result.Message });
        }

        [HttpPost]
        public IActionResult Update(ExerciseVM model)
        {
            var result = _exerciseService.UpdateExercise(model);
            return Json(new { success = result.Success, message = result.Message });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _exerciseService.DeleteExercise(id);
            return Json(new { success = result.Success, message = result.Message });
        }

        [HttpGet]
        public IActionResult GetPaged(int page = 1, int pageSize = 6, string? search = null)
        {
            var (data, totalItems) = _exerciseService.GetPagedExercises(page, pageSize, search);
            return Json(new
            {
                success = true,
                data,
                currentPage = page,
                totalPages = (int)System.Math.Ceiling((double)totalItems / pageSize)
            });
        }

        [HttpGet]
        public IActionResult GetAllMuscles()
        {
            var data = _exerciseService.GetAllMuscles();
            return Json(new { success = true, data });
        }

        [HttpGet]
        public IActionResult GetAllEquipments()
        {
            var data = _exerciseService.GetAllEquipments();
            return Json(new { success = true, data });
        }
    }

}
