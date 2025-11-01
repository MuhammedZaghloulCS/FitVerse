using FitVerse.Core.ViewModels.Anatomy;
using FitVerse.Core.ViewModels.Equipment;
using FitVerse.Data.Models;
using System.Collections.Generic;

namespace FitVerse.Core.IService
{
    public interface IAnatomyService : IService
    {
        (bool Success, string Message) AddAnatomy(AddAnatomyVM model); //برتجع تابل tuble    }
        List<AddAnatomyVM> GetAll(string? search);
        public AddAnatomyVM GetById(int id);
        (bool Success, string Message) Delete(int id);
        (bool Success, string Message) Update(AddAnatomyVM model);

       public int GetAllCount();
       public int GetMuscleCount();
        public int GetExerciseCount();
        

        
    }
}
