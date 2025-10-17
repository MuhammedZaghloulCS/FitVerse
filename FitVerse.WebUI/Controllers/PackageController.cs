using AutoMapper;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Anatomy;
using FitVerse.Core.ViewModels.Package;
using FitVerse.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class PackageController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public readonly IMapper mapper;
        public PackageController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            
            var packages = unitOfWork.Packages.GetAll().ToList();
            var data = mapper.Map<List<PackageVM>>(packages);
            return Json(new { data = data });

        }
        public IActionResult GetById(int id)
        {
            var package = unitOfWork.Packages.GetById(id);
            if (package == null)
            {
                return Json(new { success = false, message = "Somthing wrong!" });
            }
            var model = mapper.Map<PackageVM>(package);
            return Json(new { success = true, data = model });
        }
        public IActionResult Create(AddPackageVM package)
        {
            var pkg = mapper.Map<Package>(package);
            unitOfWork.Packages.Add(pkg);
            Console.WriteLine($"CoachId = {pkg.CoachId}");
            if (unitOfWork.Complete() > 0)
            {
                return Json(new { success = true, message = "Package created successfully" });
            }
            return Json(new { success = false, message = "Somthing wrong!" });
        }
        public IActionResult Update(PackageVM package)
        {
            var pkg = unitOfWork.Packages.GetById(package.Id);
            if (pkg == null)
            {
                return Json(new { success = false, message = "Somthing wrong!" });
            }
            mapper.Map(package, pkg);
            unitOfWork.Packages.Update(pkg);
            if (unitOfWork.Complete() > 0)
            {
                return Json(new { success = true, message = "Package updated successfully" });
            }
            return Json(new { success = false, message = "Somthing wrong!" });
        }
        public IActionResult Delete(int id)
        {
            var pkg = unitOfWork.Packages.GetById(id);
            if (pkg == null)
            {
                return Json(new { success = false, message = "Somthing wrong!" });
            }
            unitOfWork.Packages.Delete(pkg);
            if (unitOfWork.Complete() > 0)
            {
                return Json(new { success = true, message = "Package deleted successfully" });
            }
            return Json(new { success = false, message = "Somthing wrong!" });
        }
        public IActionResult GetPaged(int page = 1, int pageSize = 5, string? search = null)
        {
            var query = unitOfWork.Packages.GetAll().AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {

                string lowerSearch = search.ToLower();
                query = query.Where(a => a.Name.ToLower().Contains(lowerSearch));
            }

            var totalItems = query.Count();
            var data = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var mappedData = mapper.Map<IEnumerable<PackageVM>>(data);

            return Json(new
            {
                data = mappedData,
                currentPage = page,
                totalPages = (int)Math.Ceiling((double)totalItems / pageSize)
            });
        }


    }
}
