using FitVerse.Core.Interfaces;
using FitVerse.Data;
using FitVerse.Data.Context;
using FitVerse.Data.Models;
using FitVerse.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FitVerse.Core.Repositories
{
    public class DailyLogRepository : GenericRepository<DailyLog>, IDailyLogRepository
    {
        public DailyLogRepository(FitVerseDbContext context) : base(context) { }
        public DbSet<DailyLog> dailyLogs => context.Set<DailyLog>();


        public IEnumerable<DailyLog> GetLogsByClient(string clientId)
        {
            return dailyLogs
                .Where(l => l.ClientId == clientId)
                .OrderByDescending(l => l.LogDate)
                .ToList();
        }

        public IEnumerable<DailyLog> GetLogsByCoach(string coachId)
        {
            return dailyLogs
                .Where(l => l.CoachId == coachId)
                .OrderByDescending(l => l.LogDate)
                .ToList();
        }
    }
}
