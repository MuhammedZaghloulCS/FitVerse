using FitVerse.Core.Interfaces;
using FitVerse.Data.Context;
using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Repositories
{
    public class DietPlanRepository: GenericRepository<DietPlan>, IDietPlanRepository
    {
        public DietPlanRepository(FitVerseDbContext context) : base(context)
        {
        }
    
    }
}
