using AutoMapper;
using FitVerse.Core.IService;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Anatomy;
using FitVerse.Core.ViewModels.Meuscle;
using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Service.Service
{
    public class AnatomyService:IAnatomyService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AnatomyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<AnatomyVM> GetAll()
        {
            var allObj = unitOfWork.Anatomies.GetAll();
            return mapper.Map<IEnumerable<AnatomyVM>>(allObj);
        }

        public AnatomyVM GetById(int id)
        {
            var anatomy = unitOfWork.Anatomies.GetById(id);
            return mapper.Map<AnatomyVM>(anatomy);
        }

        public bool Create(AddAnatomyVM model)
        {
            var anatomy = mapper.Map<Anatomy>(model);
            unitOfWork.Anatomies.Add(anatomy);
            return unitOfWork.Complete() > 0;
        }

        public bool Update(AnatomyVM model)
        {
            var anatomy = unitOfWork.Anatomies.GetById(model.Id);
            if (anatomy == null)
                return false;

            anatomy.Name = model.Name;
            unitOfWork.Anatomies.Update(anatomy);
            return unitOfWork.Complete() > 0;
        }

        public bool Delete(int id)
        {
            var anatomy = unitOfWork.Anatomies.GetById(id);
            if (anatomy == null)
                return false;

            unitOfWork.Anatomies.Delete(anatomy);
            return unitOfWork.Complete() > 0;
        }

        public (IEnumerable<AnatomyVM> Data, int CurrentPage, int TotalPages) GetPaged(int page = 1, int pageSize = 5, string? search = null)
        {
            var query = unitOfWork.Anatomies.GetAll().AsQueryable();

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

            var mappedData = mapper.Map<IEnumerable<AnatomyVM>>(data);

            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return (mappedData, page, totalPages);
        }
        public int GetAllCount()
        {
            return unitOfWork.Anatomies.GetAll().Count();
        }
        public int GetMuscleCount()
        {
            var count = unitOfWork.Muscles
            .GetAll().Count();

            return count;

        }
        public int GetExerciseCount()
        {
            var count = unitOfWork.Exercises
            .GetAll().Count();
            return count;
        }
        public int GetCountByAnatomy(int anatomyId)
        {
            var count = unitOfWork.Muscles
            .GetAll()
            .Count(m => m.AnatomyId == anatomyId);
            return count;
        }
        public List<Muscle> GetMusclesByAnatomyId(int anatomyId)
        {
            // بنجيب كل العضلات اللي ليها نفس الـ AnatomyId
            var muscles = unitOfWork.Muscles
                .GetAll()
                .Where(m => m.AnatomyId == anatomyId)
                .ToList();

            return muscles;
        }

        




    }
}
