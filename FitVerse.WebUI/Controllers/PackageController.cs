using AutoMapper;
using FitVerse.Core.IService;
using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.ViewModels.Package;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class PackageController : Controller
    {
        private readonly IUnitOFWorkService unitOFWorkService;
        private readonly IPackageAppService packageService;
        private readonly IMapper mapper;

        public PackageController(IUnitOFWorkService unitOFWorkService, IMapper mapper)
        {
            this.unitOFWorkService = unitOFWorkService;
            this.packageService = unitOFWorkService.PackageAppService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        // ================== GET PAGED ==================
        [HttpGet]
        public IActionResult GetPaged(int page = 1, int pageSize = 5, string search = "")
        {
            var data = packageService.GetPaged(page, pageSize, search, out int totalPages);
            return Json(new { data, currentPage = page, totalPages });
        }

        // ================== GET BY ID ==================
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var pkg = packageService.GetById(id);
            if (pkg == null)
                return Json(new { success = false, message = "Package not found!" });

            return Json(new { success = true, data = pkg });
        }

        // ================== CREATE ==================
        [HttpPost]
        public IActionResult Create([FromForm] AddPackageVM package)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid data!" });

            var success = packageService.Create(package, out string message);
            return Json(new { success, message });
        }

        // ================== UPDATE ==================
        [HttpPost]
        public IActionResult Update([FromForm] PackageVM package)
        {
            var success = packageService.Update(package, out string message);
            return Json(new { success, message });
        }

        // ================== DELETE ==================
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var success = packageService.Delete(id, out string message);
            return Json(new { success, message });
        }
    }
}
