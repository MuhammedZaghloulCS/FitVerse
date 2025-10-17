
using AutoMapper;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.viewModels;
using FitVerse.Core.ViewModels;
using FitVerse.Core.ViewModels.Exercise;
using FitVerse.Data.Models;
using FitVerse.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
namespace FitVerse.Web.Controllers
{
    public class ExersiceController : Controller
    {
        IUnitOfWork db; IMapper mapper;
        public ExersiceController(IUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            if (db == null)
                throw new NullReferenceException("UnitOfWork returns NullException");
            if (db.Exercises == null)
                throw new NullReferenceException("DbContext.Excersice returns NullException");

            var data = mapper.Map<ExerciseVM>(db.Exercises.GetAll());
            return Json(new { data });
            
        }

        public IActionResult Create(AddExerciseVM exercise)
        {
            if (exercise == null)
                throw new NullReferenceException();
            var exe = mapper.Map<Exercise>(exercise);
            db.Exercises.Add(exe);
            if (db.Complete() > 0)
            {
                return Json(new { success = true, message = "Exercise created successfully" });

            }
            return Json(new { success = false, message = "Somthing wrong!" });

        }


        public IActionResult GetById(int id)
        {
           var exe= db.Exercises.GetById(id);
            if (exe == null)
                throw new NullReferenceException();
            var exercise=mapper.Map<ExerciseVM>(exe);
            return Json(new { success = true,  data =exercise } );
        }


        public IActionResult Update(EquipmentVM model)
        {
            var equipment = db.Exercises.GetById(model.Id);
            if (equipment == null)
            {
                return Json(new { success = false, message = "Not Found!" });
            }
            equipment.Name = model.Name;
            db.Exercises.Update(equipment);
            if (db.Complete() > 0)
            {
                return Json(new { success = true, message = "Equipment updated successfully" });
            }
            return Json(new { success = false, message = "Somthing wrong!" });

        }
        public IActionResult Delete(int id)
        {
            var equipment = db.Exercises.GetById(id);
            if (equipment == null)
            {
                return Json(new { success = false, message = "Not Found!" });
            }
            db.Exercises.Delete(equipment);
            if (db.Complete() > 0)
            {
                return Json(new { success = true, message = "Equipment deleted successfully" });
            }
            return Json(new { success = false, message = "Somthing wrong!" });
        }
        public IActionResult GetPaged(int page = 1, int pageSize = 5, string? search = null)
        {
            var query = db.Exercises.GetAll().AsQueryable();

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
