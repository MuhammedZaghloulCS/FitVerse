using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.IService
{
    public interface IExercisePlanDetailService
    {
        IEnumerable<ExercisePlanDetail> GetDetailsByPlanId(int planId);
        bool AddDetail(ExercisePlanDetail detail);
        bool UpdateDetail(ExercisePlanDetail detail);
        bool DeleteDetail(int id);
    }
}
