using FitVerse.Core.IService;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Specialist;
using FitVerse.Data.Models;
using FitVerse.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Service.Service
{
    public class SpecialtyService : ISpecialtyService
    {
       private readonly IUnitOfWork _unitOfWork;
       public  SpecialtyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public (bool Success, string Message) AddSpecialty(AddSpecialtyVM model)
        {
            _unitOfWork.Specialties.Add(new Specialty
            {
                Name = model.Name,
                Description = model.Description,
                Icon = model.Icon,
            });
            return _unitOfWork.Complete() > 0 ? (true, "Specialty Added Successfully") : (false, "Something Went Wrong");
        }

        public(bool Success, string Message) DeleteSpecialty(int id)
        {
            var specialty = _unitOfWork.Specialties.GetById(id);
            if (specialty == null) return (false, "Specialty Not Found");
            _unitOfWork.Specialties.Delete(specialty);
            return _unitOfWork.Complete() > 0 ? (true, "Specialty deleted successfully") : (false, "Something went wrong!");


        }
        
        public List<SpecialtyVM> GetAllSpecialties()
        {
            var allSpecialties = _unitOfWork.Specialties.GetAll().ToList();
            var allCoachSpecialties = _unitOfWork.CoachSpecialties.GetAll().ToList();

            return allSpecialties.Select(s => new SpecialtyVM
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Icon = s.Icon,
                Color = s.Color,
                CoachesCount = allCoachSpecialties.Count(cs => cs.SpecialtyId == s.Id)
            }).ToList();

           
        }

       public SpecialtyVM? GetSpecialtyById(int id)
        {
            var specialty = _unitOfWork.Specialties.GetById(id);
            var totalSpecialties = _unitOfWork.Specialties.GetAll().Count(); 

            if (specialty == null) return null;
            return new SpecialtyVM
            {
                Id = specialty.Id,
                Name = specialty.Name,
                Description = specialty.Description,
                Icon = specialty.Icon,
                CoachesCount = specialty.CoachSpecialties.Count(),
                TotalSpecialties = totalSpecialties

            };
        }

        public (int TotalSpecialties, int TotalCoaches) GetStats()
        {
            var totalSpecialties = _unitOfWork.Specialties.GetAll().Count();
            var totalCoaches = _unitOfWork.CoachSpecialties.GetAll().Count();

            return (totalSpecialties, totalCoaches);
        }

       public (bool Success, string Message) UpdateSpecialty(SpecialtyVM model)
        {
            var specialty=_unitOfWork.Specialties.GetById(model.Id);
            if (specialty == null) return (false, "Specialty Not Found");
            specialty.Name = model.Name;
            specialty.Description = model.Description;
            specialty.Icon = model.Icon;

            _unitOfWork.Specialties.Update(specialty);
            return _unitOfWork.Complete() > 0 ? (true, "specialty updated successfully") : (false, "Something went wrong!");


        }

       
    }
}
