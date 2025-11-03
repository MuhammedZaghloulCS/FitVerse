using FitVerse.Core.IService;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Equipment;
using FitVerse.Core.ViewModels.Specialist;
using FitVerse.Core.ViewModels.Specialty;
using FitVerse.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class SpecialtyController : Controller
    {
        private readonly ISpecialtyService _specialtyService;

        public SpecialtyController(ISpecialtyService specialtyService)
        {
            _specialtyService = specialtyService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var specialties = _specialtyService.GetAllSpecialties();
            return Json(new { data = specialties });
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var specialty = _specialtyService.GetSpecialtyById(id);
            if (specialty == null)
                return NotFound(new { message = "Specialty not found" });

            return Json(specialty);
        }

        [HttpPost]
        public IActionResult Create([FromForm] AddSpecialtyVM model)
        {

            try
            {
                var result = _specialtyService.AddSpecialty(model);
                return Json(new { success = true, message = "Specialty added successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }

        }


        [HttpPut]
        public IActionResult Update([FromForm] UpdateSpecialtyVM model)
        {
            var result = _specialtyService.UpdateSpecialty(model);
            return Json(new { Success = result.Success, Message = result.Message });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _specialtyService.DeleteSpecialty(id);
            return Json(result);
        }

        [HttpGet]
        public IActionResult GetStats()
        {
            var stats = _specialtyService.GetStats();
            return Json(new { totalSpecialties = stats.TotalSpecialties, totalCoaches = stats.TotalCoaches });
        }
    }

    }
