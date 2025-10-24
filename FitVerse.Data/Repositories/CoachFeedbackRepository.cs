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
    public class CoachFeedbackRepository:GenericRepository<CoachFeedback> ,ICoachFeedbackRepository
    {
        public CoachFeedbackRepository(FitVerseDbContext context) : base(context)
        {
        }
    }
}
