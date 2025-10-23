using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.ExerciseVM;
using FitVerse.Core.ViewModels.Meuscle;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // ✅ Get All Muscles
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var muscles = unitOfWork.Muscles.GetAllWithAnatomy();

                var data = muscles.Select(m => new MuscleVM
                {
                    Id = m.Id,
                    Name = m.Name,
                    //Description = m.Description ?? "No description",
                    AnatomyName = m.Anatomy != null ? m.Anatomy.Name : "Unknown",
                    ExerciseCount = m.Exercises != null ? m.Exercises.Count : 0
                }).ToList();

                return Json(new { success = true, data });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult GetAnatomyGroups()
        {
            try
            {
                var anatomies = unitOfWork.Anatomies.GetAll();

                var data = anatomies.Select(a => new
                {
                    Id = a.Id,
                    Name = a.Name
                }).ToList();

                return Json(new { success = true, data });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost] 
        public IActionResult Create(AddMuscleVM model) {
            var anatomy = unitOfWork.Anatomies.GetAll().FirstOrDefault(
            a => a.Name.ToLower() == model.AnatomyName.ToLower()); 
            if (anatomy == null) {
                return Json(new { success = false, message = "Anatomy group not found!" }); } 
            unitOfWork.Muscles.Add(  new Data.Models.Muscle {
                Name = model.Name, AnatomyId = anatomy.Id }); 
            if (unitOfWork.Complete() > 0) {
                return Json(new { success = true, message = "Muscle created successfully" }); } 
            else { return Json(new { success = false, message = "Somthing wrong!" }); } }

        // ✅ Get by Id
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var muscle = unitOfWork.Muscles.GetByIdWithAnatomy(id);
            if (muscle == null)
                return Json(new { success = false, message = "Muscle not found!" });

            var model = new MuscleVM
            {
                Id = muscle.Id,
                Name = muscle.Name,
                Description = muscle.Description,
                AnatomyId = muscle.AnatomyId,
                AnatomyName = muscle.Anatomy?.Name ?? "N/A"
            };

            return Json(new { success = true, data = model });
        }

        // ✅ Update
        [HttpPost]
        public IActionResult Update(MuscleVM model)
        {
            var muscle = unitOfWork.Muscles.GetById(model.Id);
            if (muscle == null)
                return Json(new { success = false, message = "Muscle not found!" });

            muscle.Name = model.Name;
            muscle.Description = model.Description;
            muscle.AnatomyId = model.AnatomyId;

            unitOfWork.Muscles.Update(muscle);
            if (unitOfWork.Complete() > 0)
                return Json(new { success = true, message = "Muscle updated successfully" });

            return Json(new { success = false, message = "Something went wrong!" });
        }

        // ✅ Delete
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
        public IActionResult GetPaged(int page = 1, int pageSize = 6, string? search = null)
        {
            var query = unitOfWork.Muscles.GetAll().AsQueryable();

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
            var mappedData = data.Select(e => new MuscleVM { Id = e.Id, Name = e.Name }).ToList();


            return Json(new
            {
                data = mappedData,
                currentPage = page,
                totalPages = (int)Math.Ceiling((double)totalItems / pageSize)
            });
        }
    }
}
