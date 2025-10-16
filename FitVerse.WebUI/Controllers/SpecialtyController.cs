using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Equipment;
using FitVerse.Core.ViewModels.Specialist;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class SpecialtyController : Controller
    {
        IUnitOfWork unitOfWork;
        public SpecialtyController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            var allObj = unitOfWork.Specialties.GetAll();
            var data = allObj.Select(s => new SpecialtyVM { Id = s.Id, Name = s.Name }).ToList();
            return Json(new { data = data });
        }
       
    
        public IActionResult Create(AddSpecialtyVM model)
        {
            unitOfWork.Specialties.Add(new Data.Models.Specialty { Name = model.Name });
            if (unitOfWork.Complete() > 0)
            {
                return Json(new { success = true, message = "Specialty created successfully" });

            }
            return Json(new { success = false, message = "Somthing wrong!" });

        }
        public IActionResult GetById(int id)
        {
            var specialty = unitOfWork.Specialties.GetById(id);
            if (specialty == null)
            {
                return Json(new { success = false, message = "Somthing wrong!" });
            }
            var model = new SpecialtyVM { Id = specialty.Id, Name = specialty.Name };
            return Json(new { success = true, data = model });
        }
        public IActionResult Update(SpecialtyVM model)
        {
            var specialty = unitOfWork.Specialties.GetById(model.Id);
            if (specialty == null)
            {
                return Json(new { success = false, message = "Not Found!" });
            }
            specialty.Name = model.Name;
            unitOfWork.Specialties.Update(specialty);
            if (unitOfWork.Complete() > 0)
            {
                return Json(new { success = true, message = "Specialty updated successfully" });
            }
            return Json(new { success = false, message = "Somthing wrong!" });

        }
        public IActionResult Delete(int id)
        {
            var specialty = unitOfWork.Specialties.GetById(id);
            if (specialty == null)
            {
                return Json(new { success = false, message = "Not Found!" });
            }
            unitOfWork.Specialties.Delete(specialty);
            if (unitOfWork.Complete() > 0)
            {
                return Json(new { success = true, message = "Specialty deleted successfully" });
            }
            return Json(new { success = false, message = "Somthing wrong!" });
        }
        public IActionResult GetPaged(int page = 1, int pageSize = 5, string? search = null)
        {
            var query = unitOfWork.Specialties.GetAll().AsQueryable();

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
            var mappedData = data.Select(e => new SpecialtyVM { Id = e.Id, Name = e.Name }).ToList();


            return Json(new
            {
                data = mappedData,
                currentPage = page,
                totalPages = (int)Math.Ceiling((double)totalItems / pageSize)
            });
        } }

    }
