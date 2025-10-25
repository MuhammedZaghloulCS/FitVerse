using AutoMapper;
using FitVerse.Core.Interfaces; // IUnitOfWork
using FitVerse.Core.Models; // الكلاسات الأساسية
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Package;
using FitVerse.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class PackageController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PackageController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IActionResult index()
        {
            return View();

        }

        // ================== GET PAGED ==================
        [HttpGet]
        public IActionResult GetPaged(int page = 1, int pageSize = 5, string search = "")
        {
            var query = unitOfWork.Packages.GetAll(); 

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Name.Contains(search));
            }

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var data = query
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => mapper.Map<PackageVM>(p))
                .ToList();

            return Json(new { data, currentPage = page, totalPages });
        }

        // ================== GET BY ID ==================
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var pkg = unitOfWork.Packages.GetById(id);
            if (pkg == null)
                return Json(new { success = false, message = "Package not found!" });

            var vm = mapper.Map<PackageVM>(pkg);
            return Json(new { success = true, data = vm });
        }

        // ================== CREATE ==================
        [HttpPost]
        public IActionResult Create(AddPackageVM package)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid data!" });

            var pkg = mapper.Map<Package>(package);
            unitOfWork.Packages.Add(pkg);

            if (unitOfWork.Complete() > 0)
                return Json(new { success = true, message = "Package created successfully" });

            return Json(new { success = false, message = "Something went wrong!" });
        }

        // ================== UPDATE ==================
        [HttpPost]
        public IActionResult Update(PackageVM package)
        {
            var pkg = unitOfWork.Packages.GetById(package.Id);
            if (pkg == null)
                return Json(new { success = false, message = "Package not found!" });

            mapper.Map(package, pkg);
            unitOfWork.Packages.Update(pkg);

            if (unitOfWork.Complete() > 0)
                return Json(new { success = true, message = "Package updated successfully" });

            return Json(new { success = false, message = "Something went wrong!" });
        }

        // ================== DELETE ==================
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var pkg = unitOfWork.Packages.GetById(id);
            if (pkg == null)
                return Json(new { success = false, message = "Package not found!" });

            unitOfWork.Packages.Delete(pkg);

            if (unitOfWork.Complete() > 0)
                return Json(new { success = true, message = "Package deleted successfully" });

            return Json(new { success = false, message = "Something went wrong!" });
        }
    }
}
