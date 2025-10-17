using AutoMapper;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.viewModels;
using FitVerse.Core.ViewModels.Anatomy;
using FitVerse.Core.ViewModels.Equipment;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public EquipmentController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            var allObj = unitOfWork.Equipments.GetAll();
            var data = allObj.Select(e => new EquipmentVM { Id = e.Id, Name = e.Name }).ToList();
            return Json(new { data = data });
        }

        public IActionResult Create(AddEquipmentVM model)
        {
            unitOfWork.Equipments.Add(new Data.Models.Equipment { Name = model.Name });
            if (unitOfWork.Complete() > 0)
            {
                return Json(new { success = true, message = "Equipment created successfully" });

            }
            return Json(new { success = false, message = "Somthing wrong!" });

        }
        public IActionResult GetById(int id)
        {
            var equipment = unitOfWork.Equipments.GetById(id);
            if (equipment == null)
            {
                return Json(new { success = false, message = "Somthing wrong!" });
            }
            var model = new EquipmentVM { Id = equipment.Id, Name = equipment.Name };
            return Json(new { success = true, data = model });
        }
        public IActionResult Update(EquipmentVM model)
        {
            var equipment = unitOfWork.Equipments.GetById(model.Id);
            if (equipment == null)
            {
                return Json(new { success = false, message = "Not Found!" });
            }
            equipment.Name = model.Name;
            unitOfWork.Equipments.Update(equipment);
            if (unitOfWork.Complete() > 0)
            {
                return Json(new { success = true, message = "Equipment updated successfully" });
            }
            return Json(new { success = false, message = "Somthing wrong!" });

        }
        public IActionResult Delete(int id)
        {
            var equipment = unitOfWork.Equipments.GetById(id);
            if (equipment == null)
            {
                return Json(new { success = false, message = "Not Found!" });
            }
            unitOfWork.Equipments.Delete(equipment);
            if (unitOfWork.Complete() > 0)
            {
                return Json(new { success = true, message = "Equipment deleted successfully" });
            }
            return Json(new { success = false, message = "Somthing wrong!" });
        }
        public IActionResult GetPaged(int page = 1, int pageSize = 5, string? search = null)
        {
            var query = unitOfWork.Equipments.GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {

                string lowerSearch = search.ToLower();
                query = query.Where(a => a.Name.ToLower().Contains(lowerSearch));
            }

            var totalItems = query.Count();
            var data = query
                .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
            var mappedData = data.Select(e => new EquipmentVM { Id = e.Id, Name = e.Name }).ToList();


            return Json(new
            {
                data = mappedData,
                currentPage = page,
                totalPages = (int)Math.Ceiling((double)totalItems / pageSize)
            });
        }
    }
    

}