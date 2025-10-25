using FitVerse.Data.Models;

public class ClientSubscription
{
    public int Id { get; set; }

    public string ClientId { get; set; }
    public virtual Client Client { get; set; } = null!; 

    public int PackageId { get; set; }
    public virtual Package Package { get; set; } = null!; 

    public string CoachId { get; set; }
    public virtual Coach Coach { get; set; } = null!; 

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public decimal PriceAtPurchase { get; set; }
    public string Status { get; set; } = "Active";
}
