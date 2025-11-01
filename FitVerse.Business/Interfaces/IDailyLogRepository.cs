using FitVerse.Data.Models;

namespace FitVerse.Core.Interfaces
{
    public interface IDailyLogRepository : IGenericRepository<DailyLog>
    {
        IEnumerable<DailyLog> GetLogsByClient(string clientId);
        IEnumerable<DailyLog> GetLogsByCoach(string coachId);
    }
}
