using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.ViewModels.Admin;
using FitVerse.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IActionResult Coaches()
        {
            var coaches = _unitOfWorkService.CoachService.GetAllCoachesWithPackages();
            return View(coaches);
        }

        [HttpGet]
        public IActionResult GetCoachesData(int page = 1, int pageSize = 10, string search = "")
        {
            var (coaches, totalItems) = _unitOfWorkService.CoachService.GetPagedEquipments(page, pageSize, search);
            return Json(new
            {
                success = true,
                data = coaches,
                currentPage = page,
                totalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                totalItems = totalItems
            });
        }

        public IActionResult Clients()
        {
            var clients = _unitOfWorkService.clientOnCoachesService.GetAllClients();
            return View(clients);
        }

        [HttpGet]
        public IActionResult GetClientsData(int page = 1, int pageSize = 10, string search = "")
        {
            var allClients = _unitOfWorkService.clientOnCoachesService.GetAllClients();
            
            // Apply search filter
            if (!string.IsNullOrEmpty(search))
            {
                allClients = allClients.Where(c => c.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            var totalItems = allClients.Count;
            var clients = allClients.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return Json(new
            {
                success = true,
                data = clients,
                currentPage = page,
                totalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                totalItems = totalItems
            });
        }

    }
}
