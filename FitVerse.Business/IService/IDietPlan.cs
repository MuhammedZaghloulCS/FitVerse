using FitVerse.Core.ViewModels.DietPlan;
using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.IService
{
    public interface IDietPlan:IService
    {
        IEnumerable<DietPlanVM> GetAll();
        DietPlanVM? GetById(int id);
        void Add(DietPlanVM plan);
        void Update(DietPlanVM plan);
        void Delete(int id);
        int GetCount();
        int GetClientFollowing();
        

    }
}
