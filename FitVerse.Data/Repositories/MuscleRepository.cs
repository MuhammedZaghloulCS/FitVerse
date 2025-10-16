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
    public class MuscleRepository:GenericRepository<Muscle>, IMuscleRepository
    {
      
        private readonly FitVerseDbContext _context;

        public MuscleRepository(FitVerseDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Muscle> GetAllWithAnatomy()
        {
            return _context.Muscles
                .Include(m => m.Anatomy)
                .ToList();
        }
        public Muscle GetByIdWithAnatomy(int id)
        {
            return _context.Muscles
                .Include(m => m.Anatomy)
                .FirstOrDefault(m => m.Id == id);
        }
    }
}
