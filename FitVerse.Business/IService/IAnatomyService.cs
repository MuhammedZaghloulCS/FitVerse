using FitVerse.Core.ViewModels.Anatomy;
using FitVerse.Core.ViewModels.Meuscle;
using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.IService
{
    public interface IAnatomyService:IService
    {
        IEnumerable<AnatomyVM> GetAll();
        AnatomyVM GetById(int id);
        bool Create(AddAnatomyVM model);
        bool Update(AnatomyVM model);
        bool Delete(int id);
        (IEnumerable<AnatomyVM> Data, int CurrentPage, int TotalPages) GetPaged(int page = 1, int pageSize = 5, string? search = null);
        public int GetAllCount();
        public int GetMuscleCount();
        public int GetExerciseCount();
        public int GetCountByAnatomy(int anatomyId);
        List<Muscle> GetMusclesByAnatomyId(int anatomyId);

    }
}
