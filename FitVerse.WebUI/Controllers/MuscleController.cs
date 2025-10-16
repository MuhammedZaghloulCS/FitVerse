using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Meuscle;
using FitVerse.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class MuscleController : Controller
    {
        private IUnitOfWork unitOfWork;

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
                Anatomygroup = m.Anatomy != null ? m.Anatomy.Name : "N/A"
            }).ToList();

            return Json(new { data = data });
        }
        [HttpPost]
        public IActionResult Create(AddMuscleVM model)
        {
            var anatomy = unitOfWork.Anatomies.GetAll().FirstOrDefault(a => a.Name.ToLower() == model.Anatomygroup.ToLower());
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
        public IActionResult GetAnatomyGroups()
        {
            var groups = unitOfWork.Anatomies.GetAll()
                .Select(a => new { a.Id, a.Name })
                .ToList();

            return Json(new { data = groups });
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var muscle = unitOfWork.Muscles.GetByIdWithAnatomy(id);
            if (muscle == null)
            {
                return Json(new { success = false, message = "Somthing wrong!" });
            }
            var model = new MuscleVM { Id = muscle.Id, Name = muscle.Name, Anatomygroup = muscle.Anatomy.Name };
            return Json(new { success = true, data = model });
        }
        [HttpPost]
        public IActionResult Update(MuscleVM model)
        {
            var muscle = unitOfWork.Muscles.GetById(model.Id);
            if (muscle == null)
                return Json(new { success = false, message = "Muscle not found!" });

            var anatomy = unitOfWork.Anatomies.GetById(model.AnatomyId);
            if (anatomy == null)
                return Json(new { success = false, message = "Anatomy group not found!" });

            muscle.Name = model.Name;
            muscle.AnatomyId = anatomy.Id;

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
            {
                return Json(new { success = false, message = "Not Found!" });
            }
            unitOfWork.Muscles.Delete(muscle);
            if (unitOfWork.Complete() > 0)
            {
                return Json(new { success = true, message = "Muscle deleted successfully" });
            }
            return Json(new { success = false, message = "Somthing wrong!" });
        }
    }
}
