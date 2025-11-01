using AutoMapper;
using FitVerse.Core.IService;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.DietPlan;
using FitVerse.Data.Models;
using FitVerse.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Service.Service
{
    public class DietPlanService:IDietPlan
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        

        public DietPlanService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

       

        public void Add(DietPlanVM plan)
        {
            // ✅ جلب بيانات العميل المرتبط بالخطة
            //var client = _unitOfWork.Clients.GetAll().FirstOrDefault(c => c.Id == plan.ClientId);

            //if (client == null)
            //    throw new Exception("Client not found.");
            double weight = plan.Weight;
            double height = plan.Height;
            // ✅ حساب BMR
            double bmr;
            if (plan.Gender.ToLower() == "male")
                bmr = 10 * weight + 6.25 * height- 5 * plan.Age + 5;
            else
                bmr = 10 * weight + 6.25 * height- 5 * plan.Age - 161;

            // ✅ حساب TDEE
            double tdee = bmr * plan.ActivityMultiplier;

            // ✅ حساب الماكروز
            double proteinCal = tdee * 0.30;
            double carbsCal = tdee * 0.50;
            double fatsCal = tdee * 0.20;

            // ✅ تحويل للسعرات بالغرامات
            double proteinGrams = proteinCal / 4;
            double carbsGrams = carbsCal / 4;
            double fatsGrams = fatsCal / 9;


            double totalMacroCalories = (proteinGrams * 4) + (carbsGrams * 4) + (fatsGrams * 9);
            double proteinPercent = (proteinGrams * 4 / totalMacroCalories) * 100;
            double carbPercent = (carbsGrams * 4 / totalMacroCalories) * 100;
            double fatPercent = (fatsGrams * 9 / totalMacroCalories) * 100;

            // ✅ ملء بيانات الخطة
            plan.TotalCal = Math.Round(tdee, 0);
            plan.ProteinInGrams = Math.Round(proteinGrams, 1);
            plan.CarbInGrams = Math.Round(carbsGrams, 1);
            plan.FatsInGrams = Math.Round(fatsGrams, 1);
            plan.ProteinPercentage = Math.Round(proteinPercent, 1);
            plan.CarbPercentage = Math.Round(carbPercent, 1);
            plan.FatPercentage = Math.Round(fatPercent, 1);

            var dietPlan = _mapper.Map<DietPlan>(plan);
            _unitOfWork.DietPlans.Add(dietPlan);
            _unitOfWork.Complete();

        }

        public void Delete(int id)
        {
            var dietPlan = _unitOfWork.DietPlans.GetById(id);
            if (dietPlan != null)
            {
                _unitOfWork.DietPlans.Delete(dietPlan);
                _unitOfWork.Complete();
            }

        }

        public IEnumerable<DietPlanVM> GetAll()
        {
            var dietPlans = _unitOfWork.DietPlans.GetAll();
            return _mapper.Map<IEnumerable<DietPlanVM>>(dietPlans);

        }

        public DietPlanVM? GetById(int id)
        {
            var dietPlan = _unitOfWork.DietPlans.GetById(id);
            return _mapper.Map<DietPlanVM>(dietPlan);

        }

        public int GetCount()
        {
            return _unitOfWork.DietPlans.GetAll().Count();

        }

        public void Update(DietPlanVM plan)
        {
            var dietPlan = _mapper.Map<DietPlan>(plan);
            _unitOfWork.DietPlans.Update(dietPlan);
            _unitOfWork.Complete();


        }
        public int GetClientFollowing()
        {
            return _unitOfWork.Clients.GetAll().Count();
        }
       
        



    }
}
