using FitVerse.Core.Interfaces;
using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Repositories
{
    public class MuscleRepositorie:GenericRepository<Muscle>, IMuscleRepository
    {
        public MuscleRepositorie(Context.FitVerseDbContext context) : base(context)
        {
        }
    }
}
