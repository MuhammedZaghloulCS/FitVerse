public class ClientDailyLogSimpleVM
{
    public string LogId { get; set; } = Guid.NewGuid().ToString();
    public string ClientName { get; set; } = string.Empty;
    public string ClientAvatarUrl { get; set; } = "https://ui-avatars.com/api/?name=Client&background=6366f1&color=fff";
    public DateTime LogDate { get; set; }
    public string Status { get; set; } = "Pending"; // Pending أو Reviewed
    public string Notes { get; set; } = string.Empty;
    public float? Weight { get; set; }
    public int? EnergyLevel { get; set; } // 1-5
    public int? CoachRating { get; set; }
    public string? CoachFeedback { get; set; }
}
