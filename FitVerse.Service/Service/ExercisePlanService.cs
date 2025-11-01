//using FitVerse.Core.IService;
//using FitVerse.Data.Models;
//using FitVerse.Core.UnitOfWork;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//public class  : IExercisePlanService
//{
//	private readonly IUnitOfWork _unitOfWork;

//	public ExercisePlanService(IUnitOfWork unitOfWork)
//	{
//		_unitOfWork = unitOfWork;
//	}

//	// تمارين اليوم للعميل
//	public List<ExercisePlanDetail> GetTodayExercises(string clientId)
//	{
//		var today = DateTime.UtcNow.Date;

//		var plans = _unitOfWork.ExercisePlans
//			.Find(p => p.ClientId == clientId && p.Date.Date <= today && p.Date.AddDays(p.DurationInDays) >= today)
//			.ToList();

//		var exercises = plans
//			.SelectMany(p => p.ExercisePlanDetails)
//			.Where(d => d.Date.Date == today)
//			.ToList();

//		return exercises;
//	}

//	// كل خطط العميل
//	public List<ExercisePlan> GetClientPlans(string clientId)
//	{
//		return _unitOfWork.ExercisePlans
//			.Find(p => p.ClientId == clientId)
//			.ToList();
//	}
//}
