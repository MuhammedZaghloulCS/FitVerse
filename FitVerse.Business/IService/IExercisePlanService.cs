using FitVerse.Core.ViewModels.ExerciseVM;
using FitVerse.Core.ViewModels.Plan;
using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.IService
{
    public interface IExercisePlanService
    {
        IEnumerable<ExercisePlan> GetAllPlans();
        ExercisePlan GetPlanById(int id);
        bool CreatePlan(ExercisePlanVM vm);
        bool UpdatePlan(int id, ExercisePlanVM vm);
        bool DeletePlan(int id);

    }
}
