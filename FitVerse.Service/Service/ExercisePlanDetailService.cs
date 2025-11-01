//using FitVerse.Core.IService;
//using FitVerse.Core.UnitOfWork;
//using FitVerse.Data.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;

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



//        // إضافة تمرين جديد
//        public void AddExercisePlanDetail(ExercisePlanDetail exercise)
//        {
//            _unitOfWork.ExercisePlanDetails.Add(exercise);
//            _unitOfWork.Complete();
//        }

//        public List<ExercisePlanDetail> GetTodayWorkouts(string clientId)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
