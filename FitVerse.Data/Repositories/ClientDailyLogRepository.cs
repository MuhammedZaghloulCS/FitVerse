//using FitVerse.Data.Context;

//public class ClientDailyLogRepository : IClientDailyLogRepository
//{
//    private readonly FitVerseDbContext _context;

//    public ClientDailyLogRepository(FitVerseDbContext context)
//    {
//        _context = context;
//    }

//    public IEnumerable<ClientDailyLog> GetLogsByClientId(string clientId) =>
//        _context.ClientDailyLogs.Where(x => x.ClientId == clientId).ToList();

//    public IEnumerable<ClientDailyLog> GetLogsByCoachId(string coachId) =>
//        _context.ClientDailyLogs.ToList(); // هنا Dummy أو حسب المنطق

//    public void Add(ClientDailyLog log) => _context.ClientDailyLogs.Add(log);

//    public void Update(ClientDailyLog log) => _context.ClientDailyLogs.Update(log);

//    public void Remove(ClientDailyLog log) => _context.ClientDailyLogs.Remove(log);

//    public IEnumerable<ClientDailyLog> GetAllWithClients()
//    {
//        return _context.ClientDailyLogs.Include(x => x.Client).ToList();
//    }
//}
