using FitVerse.Core.Interfaces;
using FitVerse.Data.Context;
using FitVerse.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Repositories
{
    public class ExercisePlanRepository : GenericRepository<FitVerse.Data.Models.ExercisePlan>, IExercisePlanRepository
    {
        private readonly FitVerseDbContext _context;
        public ExercisePlanRepository(FitVerseDbContext context) : base(context)
        {
            this._context = context;
        }
        public IEnumerable<ExercisePlan> GetAllWithDetails()
        {
            return _context.ExercisePlans
                           .Include(p => p.ExercisePlanDetails)
                           .ToList();
        }
    }
}
