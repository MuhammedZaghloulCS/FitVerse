using FitVerse.Core.ViewModels.Meuscle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.IService
{
    public interface IMuscleService :IService
    {
        (bool Success, string Message) AddMuscle(AddMuscleVM model);
        List<MuscleVM> GetAllMuscles();
        MuscleVM? GetMuscleById(int id);
        (bool Success, string Message) UpdateMuscle(MuscleVM model);
        (bool Success, string Message) DeleteMuscle(int id);
        (List<MuscleVM> Data, int TotalItems) GetPagedMuscles(int page, int pageSize, string? search, int? anatomyId);
        (int TotalMuscles, int TotalAnatomyGroups, int TotalExercises) GetStats();
    }
}
