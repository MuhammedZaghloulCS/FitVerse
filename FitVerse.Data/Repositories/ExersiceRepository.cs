using FitVerse.Core.Interfaces;
using FitVerse.Data.Context;
using FitVerse.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Repositories
{
    public class ExersiceRepository : GenericRepository<Exercise>,IExerciseRepository
    {
        FitVerseDbContext contex;
        public ExersiceRepository(FitVerseDbContext context) : base(context)
        {
            this.contex = context;  
        }

    }
}
