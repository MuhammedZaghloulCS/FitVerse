using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.ViewModels.Admin;
using FitVerse.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitVerse.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUnitOFWorkService _unitOfWorkService;

        public AdminController(IUnitOFWorkService unitOFWorkService)
        {
            _unitOfWorkService = unitOFWorkService;
        }

        public IActionResult Index()
        {
            var model = new AdminDashboardViewModel
            {
                TotalUsers = _unitOfWorkService.AdminService.getTotalUsersCount(),
                TotalRevenue = _unitOfWorkService.AdminService.getTotalRevenue(),
                TotalCoaches = _unitOfWorkService.AdminService.getCoachesCount(),
                SoldedPackages = _unitOfWorkService.AdminService.getSoldedPackagesCount(),
                TopCoaches = _unitOfWorkService.AdminService.getTopRatedCoaches()
            };

            return View(model);
        }

        public IActionResult CoachPackages()
        {
            var coaches = _unitOfWorkService.CoachService.GetAllCoachesWithPackages();
            var packages = _unitOfWorkService.PackageAppService.GetAllPackages();
            ViewBag.Packages = packages;
            return View(coaches);
        }



        [HttpPost]
        public async Task<IActionResult> AssignPackagesToCoach(string coachId, List<int> selectedPackages)
        {
            _unitOfWorkService.PackageAppService.AssignPackagesToCoach(coachId, selectedPackages);
            TempData["SuccessMessage"] = "Packages updated successfully!";
            return RedirectToAction(nameof(CoachPackages));
        }


    }
}
