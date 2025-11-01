using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.Interfaces
{
    public interface IExercisePlanRepository: IGenericRepository<Data.Models.ExercisePlan>
    {
        IEnumerable<ExercisePlan> GetAllWithDetails();

    }
}
