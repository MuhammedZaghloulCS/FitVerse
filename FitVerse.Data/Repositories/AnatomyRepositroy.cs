using FitVerse.Core.Interfaces;
using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Repositories
{
    public class AnatomyRepositroy : GenericRepository<Anatomy>, IAnatomyRepository
    {
        public AnatomyRepositroy(Context.FitVerseDbContext context) : base(context)
        {
        }

    }
}
