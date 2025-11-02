
using FitVerse.Core.IService;
using FitVerse.Core.UnitOfWork;
using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Service.Service
{
    public class ExercisePlanDetailService : IExercisePlanDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExercisePlanDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ExercisePlanDetail> GetDetailsByPlanId(int planId)
        {
            return _unitOfWork.ExercisePlanDetails
                .Find(e => e.ExercisePlanId == planId);
        }

        public bool AddDetail(ExercisePlanDetail detail)
        {
            try
            {
                _unitOfWork.ExercisePlanDetails.Add(detail);
                _unitOfWork.Complete();
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
                _unitOfWork.ExercisePlanDetails.Update(detail);
                _unitOfWork.Complete();
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
                var detail = _unitOfWork.ExercisePlanDetails.GetById(id);
                if (detail == null) return false;

                _unitOfWork.ExercisePlanDetails.Delete(detail);
                _unitOfWork.Complete();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
