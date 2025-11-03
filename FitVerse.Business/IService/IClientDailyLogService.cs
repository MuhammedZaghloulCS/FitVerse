public interface IClientDailyLogService
{
    List<ClientDailyLogSimpleVM> GetDailyLogs(string? coachId = null, string? clientId = null);

    void AddCoachReview(string logId, int rating, string feedback);

    void AddClientLog(string clientId, string notes, float? weight, int? energyLevel);
}
