using FitVerse.Core.viewModels;
using FitVerse.Core.ViewModels.ExerciseVM;
using FitVerse.Core.ViewModels.Meuscle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.IService
{
    public interface IExerciseService : IService
    {
        List<ExerciseVM> GetAllExercises();
        ExerciseVM GetExerciseById(int id);
        (List<ExerciseVM> Data, int TotalItems) GetPagedExercises(int page, int pageSize, string? search);
        (bool Success, string Message) AddExercise(AddExerciseVM model);
        (bool Success, string Message) UpdateExercise(ExerciseVM model);
        (bool Success, string Message) DeleteExercise(int id);

        List<MuscleVM> GetAllMuscles();
        List<EquipmentVM> GetAllEquipments();
    }
}