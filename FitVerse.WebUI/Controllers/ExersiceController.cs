
using AutoMapper;
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
        IUnitOfWork db; IMapper mapper;
        public ExerciseController(IUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            GetAll();
            return View();
        }
        public IActionResult GetAll()
        {
            if (db == null)
                throw new NullReferenceException("UnitOfWork returns NullException");
            if (db.Exercises == null)
                throw new NullReferenceException("DbContext.Excersice returns NullException");

            var data = mapper.Map < List < ExerciseVM >> (db.Exercises.GetAll().ToList());
            return Json(new { data });

        }
        public IActionResult GetAllMuscles()
        {
            var muscles = db.Muscles.GetAll();
            var data = mapper.Map<MuscleVM>(muscles);
            return Json(new { data });
        }
        public IActionResult GetAllEquipments()
        {
            var Exercises = db.Equipments.GetAll();
            var data = mapper.Map<ExerciseVM>(Exercises);
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
            var exe = db.Exercises.GetById(id);
            if (exe == null)
                throw new NullReferenceException();
            var exercise = mapper.Map<ExerciseVM>(exe);
            return Json(new { success = true, data = exercise });
        }


        public IActionResult Update(ExerciseVM model)
        {
            var exercise = db.Exercises.GetById(model.Id);
            if (exercise == null)
            {
                return Json(new { success = false, message = "Not Found!" });
            }
            exercise.Name = model.Name;
            db.Exercises.Update(exercise);
            if (db.Complete() > 0)
            {
                return Json(new { success = true, message = "Exercise updated successfully" });
            }
            return Json(new { success = false, message = "Somthing wrong!" });

        }
        public IActionResult Delete(int id)
        {
            var exercise = db.Exercises.GetById(id);
            if (exercise == null)
            {
                return Json(new { success = false, message = "Not Found!" });
            }
            db.Exercises.Delete(exercise);
            if (db.Complete() > 0)
            {
                return Json(new { success = true, message = "Exercise deleted successfully" });
            }
            return Json(new { success = false, message = "Somthing wrong!" });
        }
        public IActionResult GetPaged(int page = 1, int pageSize = 6, string? search = null)
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
            var mappedData = data.Select(e => new ExerciseVM { Id = e.Id, Name = e.Name }).ToList();


            return Json(new
            {
                data = mappedData,
                currentPage = page,
                totalPages = (int)Math.Ceiling((double)totalItems / pageSize)
            });
        }
    }

}
