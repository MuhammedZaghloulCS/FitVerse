using AutoMapper;
using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Admin;
using FitVerse.Data.Models;
using FitVerse.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{

 
    public class AdminController : Controller
    {
        IUnitOFWorkService unitOFWorkService;

        public AdminController(IUnitOFWorkService unitOFWorkService)
        {
            this.unitOFWorkService = unitOFWorkService;
        }

        public IActionResult Index()
        {
            var model = new AdminDashboardViewModel
            {
                TotalUsers = unitOFWorkService.AdminService.getTotalUsersCount(),
                TotalRevenue = unitOFWorkService.AdminService.getTotalRevenue(),
                TotalCoaches = unitOFWorkService.AdminService.getCoachesCount(),
                SoldedPackages = unitOFWorkService.AdminService.getSoldedPackagesCount(),
                TopCoaches = unitOFWorkService.AdminService.getTopRatedCoaches()
            };

            return View(model);
        }

 
    }
}
