using FitVerse.Core.ViewModels.Package;

public interface IPackageAppService
{
    IEnumerable<PackageVM> GetPaged(int page, int pageSize, string search, out int totalPages);
    
    PackageVM? GetById(int id);
    bool Create(AddPackageVM package, out string message);
    bool Update(PackageVM package, out string message);
    bool Delete(int id, out string message);
    public List<PackageVM> GetAllPackages();
    public void  AssignPackagesToCoach(string coachId, List<int> selectedPackageIds);


}