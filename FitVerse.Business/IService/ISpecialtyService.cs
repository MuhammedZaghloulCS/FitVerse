using FitVerse.Core.ViewModels.Meuscle;
using FitVerse.Core.ViewModels.Specialist;
using FitVerse.Core.ViewModels.Specialty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.IService
{
    public interface ISpecialtyService :IService
    {
        (bool Success, string Message) AddSpecialty(AddSpecialtyVM model);
        List<SpecialtyVM> GetAllSpecialties();
        SpecialtyVM? GetSpecialtyById(int id);
        (bool Success, string Message) UpdateSpecialty(UpdateSpecialtyVM model);
        (bool Success, string Message) DeleteSpecialty(int id);
        (int TotalSpecialties, int TotalCoaches) GetStats();
    }
}
