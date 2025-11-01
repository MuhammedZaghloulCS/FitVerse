using AutoMapper;
using FitVerse.Core.Interfaces;
using FitVerse.Core.IService;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.viewModels;
using FitVerse.Core.ViewModels;
using FitVerse.Core.ViewModels.ExerciseVM;
using FitVerse.Core.ViewModels.Meuscle;
using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Service
{
    public class ExerciseService:IExerciseService {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public ExerciseService(IUnitOfWork db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public List<ExerciseVM> GetAllExercises()
        {
            var exercises = _db.Exercises.GetAll().ToList();
            return _mapper.Map<List<ExerciseVM>>(exercises);
        }

        public ExerciseVM GetExerciseById(int id)
        {
            var exercise = _db.Exercises.GetById(id);
            return exercise == null ? null : _mapper.Map<ExerciseVM>(exercise);
        }

        public (List<ExerciseVM> Data, int TotalItems) GetPagedExercises(int page, int pageSize, string? search)
        {
            var query = _db.Exercises.GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(e => e.Name.ToLower().Contains(search.ToLower()));

            int totalItems = query.Count();
            var items = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var data = _mapper.Map<List<ExerciseVM>>(items);
            return (data, totalItems);
        }

        public (bool Success, string Message) AddExercise(AddExerciseVM model)
        {
            var exercise = _mapper.Map<Exercise>(model);
            _db.Exercises.Add(exercise);
            return _db.Complete() > 0
                ? (true, "Exercise created successfully")
                : (false, "Something went wrong!");
        }

        public (bool Success, string Message) UpdateExercise(ExerciseVM model)
        {
            var exercise = _db.Exercises.GetById(model.Id);
            if (exercise == null)
                return (false, "Exercise not found!");

            _mapper.Map(model, exercise);
            _db.Exercises.Update(exercise);
            return _db.Complete() > 0
                ? (true, "Exercise updated successfully")
                : (false, "Something went wrong!");
        }

        public (bool Success, string Message) DeleteExercise(int id)
        {
            var exercise = _db.Exercises.GetById(id);
            if (exercise == null)
                return (false, "Exercise not found!");

            _db.Exercises.Delete(exercise);
            return _db.Complete() > 0
                ? (true, "Exercise deleted successfully")
                : (false, "Something went wrong!");
        }

        public List<MuscleVM> GetAllMuscles()
        {
            var muscles = _db.Muscles.GetAll().ToList();
            return _mapper.Map<List<MuscleVM>>(muscles);
        }

        public List<EquipmentVM> GetAllEquipments()
        {
            var equipments = _db.Equipments.GetAll().ToList();
            return _mapper.Map<List<EquipmentVM>>(equipments);
        }
    }
}
