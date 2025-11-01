using AutoMapper;
using FitVerse.Core.IService;
using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.viewModels;
using FitVerse.Core.ViewModels.Anatomy;
using FitVerse.Core.ViewModels.Equipment;
using FitVerse.Data.Models;
using FitVerse.Data.UnitOfWork;
using FitVerse.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly IUnitOFWorkService unitOfWorkservice;
       

        public EquipmentController(IUnitOFWorkService unitOfWorkservice)
        {
            this.unitOfWorkservice = unitOfWorkservice;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAll(string? search)
        {
            var equipments = unitOfWorkservice.EquipmentService.GetAll(search);
            return Json(new { data = equipments });

        }

        [HttpPost]
        public IActionResult AddEquipment([FromForm] AddEquipmentVM model)
        {
            var result = unitOfWorkservice.EquipmentService.AddEquipment(model);

            return Json(new
            {
                success = result.Success,
                message = result.Message,
                data = result.Success ? model : null
            });
        }
        public IActionResult GetById(int id)
        {
            var equipment = unitOfWorkservice.EquipmentService.GetById(id);
            if (equipment == null)
            {
                return Json(new { success = false, message = "Somthing wrong!" });
            }
            
            return Json(new { success = true, data = equipment });
        }
        public IActionResult Update(AddEquipmentVM model)
        {
            var equipment = unitOfWorkservice.EquipmentService.Update(model);
            return Json(new { success = true, message = "Equipment updated successfully" });


        }
        public IActionResult Delete(int id)
        {
            var result = unitOfWorkservice.EquipmentService.Delete(id);
            return Json(new { success = result.Success, message = result.Message });

        }
      
        public IActionResult GetTotalCountEquipment()
        {
            int totalCount = unitOfWorkservice.EquipmentService.GetTotalEquipmentCount();
            return Json(new { totalCount });
        }
        public IActionResult GetTotalCountExercise()
        {
            int totalCount = unitOfWorkservice.EquipmentService.GetTotalExerciseCount();
            return Json(new { totalCount });
        }
    }
    

}