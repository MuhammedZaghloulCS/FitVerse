using FitVerse.Core.Interfaces;
using FitVerse.Core.IService;
using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Service.Service
{
    public class ExercisePlanDetailService  : IExercisePlanDetailService
    {
        private readonly IExercisePlanDetailRepository _detailRepo;

        public ExercisePlanDetailService(IExercisePlanDetailRepository detailRepo)
        {
            _detailRepo = detailRepo;
        }

        public IEnumerable<ExercisePlanDetail> GetDetailsByPlanId(int planId)
        {
            return _detailRepo.GetAll().Where(d => d.ExercisePlanId == planId);
        }

        public bool AddDetail(ExercisePlanDetail detail)
        {
            try
            {
                _detailRepo.Add(detail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateDetail(ExercisePlanDetail detail)
        {
            try
            {
                _detailRepo.Update(detail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteDetail(int id)
        {
            try
            {
                var detail = _detailRepo.GetById(id);
                if (detail == null) return false;

                _detailRepo.Delete(detail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
