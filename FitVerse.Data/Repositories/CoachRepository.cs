using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Repositories
{
    public class CoachRepository : GenericRepository<Data.Models.Coach>, FitVerse.Core.Interfaces.ICoachRepository
    {
        public CoachRepository(Context.FitVerseDbContext context) : base(context)
        {
        }
    }

}
