using FitVerse.Data.Models;

public class CoachPackage
{
    public string CoachId { get; set; }
    public virtual Coach Coach { get; set; } = null!;  

    public int PackageId { get; set; }
    public virtual Package Package { get; set; } = null!; 
}
