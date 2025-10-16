using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Meuscle;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class MuscleController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public MuscleController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var allobj = unitOfWork.Muscles.GetAllWithAnatomy();

            var data = allobj.Select(m => new MuscleVM
            {
                Id = m.Id,
                Name = m.Name,
                AnatomyName = m.Anatomy != null ? m.Anatomy.Name : "N/A"
            }).ToList();

            return Json(new { data = data });
        }
        public IActionResult GetAnatomyGroups()
        {
            var anatomies = unitOfWork.Anatomies.GetAll();
            var data = anatomies.Select(a => new
            {
                id = a.Id,
                name = a.Name
            }).ToList();

            return Json(new { data });
        }

        [HttpPost]
        public IActionResult Create(AddMuscleVM model)
        {
            var anatomy = unitOfWork.Anatomies.GetAll()
                .FirstOrDefault(a => a.Name.ToLower() == model.AnatomyName.ToLower());
            if (anatomy == null)
            {
                return Json(new { success = false, message = "Anatomy group not found!" });
            }
            unitOfWork.Muscles.Add(new Data.Models.Muscle { Name = model.Name, AnatomyId = anatomy.Id });

            if (unitOfWork.Complete() > 0)
            {
                return Json(new { success = true, message = "Muscle created successfully" });
            }
            else
            {
                return Json(new { success = false, message = "Somthing wrong!" });
            }
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var muscle = unitOfWork.Muscles.GetById(id);
            if (muscle == null)
                return Json(new { success = false, message = "Muscle not found!" });

            var model = new MuscleVM
            {
                Id = muscle.Id,
                Name = muscle.Name,
                AnatomyId = muscle.AnatomyId,
                AnatomyName = muscle.Anatomy != null ? muscle.Anatomy.Name : "N/A"
            };

            return Json(new { success = true, data = model });
        }

      
        [HttpPost]
        public IActionResult Update(MuscleVM model)
        {
            var muscle = unitOfWork.Muscles.GetById(model.Id);
            if (muscle == null)
                return Json(new { success = false, message = "Muscle not found!" });

            muscle.Name = model.Name;
            if (model.AnatomyId != 0)
                muscle.AnatomyId = model.AnatomyId;

            unitOfWork.Muscles.Update(muscle);
            if (unitOfWork.Complete() > 0)
                return Json(new { success = true, message = "Muscle updated successfully" });

            return Json(new { success = false, message = "Something went wrong!" });
        }

     
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var muscle = unitOfWork.Muscles.GetById(id);
            if (muscle == null)
                return Json(new { success = false, message = "Muscle not found!" });

            unitOfWork.Muscles.Delete(muscle);
            if (unitOfWork.Complete() > 0)
                return Json(new { success = true, message = "Muscle deleted successfully" });

            return Json(new { success = false, message = "Something went wrong!" });
        }

        [HttpGet]
        public IActionResult GetPaged(int page = 1, int pageSize = 5, string? search = null)
        {
             var query = unitOfWork.Muscles.GetAllWithAnatomy();

           if (!string.IsNullOrEmpty(search))
        {
        string lowerSearch = search.ToLower();
        query = query.Where(m => m.Name.ToLower().Contains(lowerSearch));
        }

            var totalItems = query.Count();
            var data = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var mappedData = data.Select(m => new MuscleVM
            {
                Id = m.Id,
                Name = m.Name,
                AnatomyName = m.Anatomy != null ? m.Anatomy.Name : "N/A"
            }).ToList();

            return Json(new
            {
                data = mappedData,
                currentPage = page,
                totalPages = (int)Math.Ceiling((double)totalItems / pageSize)
            });
        }
    }
}
