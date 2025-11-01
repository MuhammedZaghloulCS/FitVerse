
//namespace FitVerse.Service.Service
//{
//    public class ExercisePlanDetailService : IExercisePlanDetailService
//    {
//        private readonly IUnitOfWork _unitOfWork;

//        public ExercisePlanDetailService(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        // جلب كل التمارين الخاصة بالعميل
//        public List<ExercisePlanDetail> GetClientExercises(string clientId)
//        {
//            return _unitOfWork.ExercisePlanDetails
//                .Find(e => e.ExercisePlan.ClientId == clientId)
//                .ToList();
//        }

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

//        // إضافة تمرين جديد
//        public void AddExercisePlanDetail(ExercisePlanDetail exercise)
//        {
//            _unitOfWork.ExercisePlanDetails.Add(exercise);
//            _unitOfWork.Complete();
//        }
        public bool DeleteDetail(int id)
        {
            try
            {
                var detail = _detailRepo.GetById(id);
                if (detail == null) return false;

//        public List<ExercisePlanDetail> GetTodayWorkouts(string clientId)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
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
