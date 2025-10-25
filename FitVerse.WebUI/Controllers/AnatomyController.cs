using AutoMapper;
using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Anatomy;
using FitVerse.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class AnatomyController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUnitOFWorkService Service;
        private readonly IMapper mapper;


        public AnatomyController(IUnitOfWork unitOfWork, IMapper mapper, IUnitOFWorkService service)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            Service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            if (unitOfWork == null)
                throw new Exception("unitOfWork is NULL!");

            if (unitOfWork.Anatomies == null)
                throw new Exception("unitOfWork.Anatomies is NULL!");

            if (mapper == null)
                throw new Exception("mapper is NULL!");

            var allObj = unitOfWork.Anatomies.GetAll();
            var data = mapper.Map<IEnumerable<AnatomyVM>>(allObj);
            return Json(new { data });
        }


        public IActionResult GetById(int id)
        {
            var anatomy = unitOfWork.Anatomies.GetById(id);
            if (anatomy == null)
            {
                return Json(new { success = false, message = "Somthing wrong!" });
            }
            var model = mapper.Map<AnatomyVM>(anatomy);
            return Json(new { success = true, data = model });

        }
        public IActionResult Create(AddAnatomyVM model)
        {
            var anatomy = mapper.Map<Anatomy>(model);
            unitOfWork.Anatomies.Add(anatomy);
            if (unitOfWork.Complete() > 0)
            {
                return Json(new { success = true, message = "Anatomy created successfully" });
            }
            return Json(new { success = false, message = "Somthing wrong!" });
        }
        public IActionResult Delete(int id)
        {
            var anatomy = unitOfWork.Anatomies.GetById(id);
            if (anatomy == null)
            {
                return Json(new { success = false, message = "Not Found!" });
            }
            unitOfWork.Anatomies.Delete(anatomy);
            if (unitOfWork.Complete() > 0)
            {
                return Json(new { success = true, message = "Anatomy deleted successfully" });
            }
            return Json(new { success = false, message = "Somthing wrong!" });
        }
        public IActionResult Update(AnatomyVM model)
        {
            var anatomy = unitOfWork.Anatomies.GetById(model.Id);
            if (anatomy == null)
            {
                return Json(new { success = false, message = "Not Found!" });
            }
            anatomy.Name = model.Name;
            unitOfWork.Anatomies.Update(anatomy);
            if (unitOfWork.Complete() > 0)
            {
                return Json(new { success = true, message = "Anatomy updated successfully" });
            }
            return Json(new { success = false, message = "Somthing wrong!" });
        }

        public IActionResult GetPaged(int page = 1, int pageSize = 5, string? search = null)
        {
            var query = unitOfWork.Anatomies.GetAll().AsQueryable();
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

            var mappedData = mapper.Map<IEnumerable<AnatomyVM>>(data);

            return Json(new
            {
                data = mappedData,
                currentPage = page,
                totalPages = (int)Math.Ceiling((double)totalItems / pageSize)
            });
        }


    }
}
