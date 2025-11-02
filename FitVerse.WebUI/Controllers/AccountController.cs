using FitVerse.Core.Helpers;
using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.Models;
using FitVerse.Core.ViewModels.Admin.Account;
using FitVerse.Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FitVerse.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOFWorkService unitOfWorkService;

        public AccountController(IUnitOFWorkService unitOfWorkService)
        {
            this.unitOfWorkService = unitOfWorkService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login auth)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var res=await unitOfWorkService.AccountService.Login(auth);
            if(!res.Success)
            {
                ModelState.AddModelError("", "Invalid Email Or Password Address");
                return View();

            }
            var role =await unitOfWorkService.AccountService.GetRole(res.User);
            if(role== Statics.ADMIN)
            {
                return RedirectToAction("Index", "Admin");
            }
            else if(role== Statics.COACH)
            {
                return RedirectToAction("Dashboard", "Coach");
            }
            else if(role== Statics.CLIENT)
            {
                return RedirectToAction("Dashboard", "ClientDashboard");
            }
            return Json(new { res.Success,res.Message,role});
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(Register auth)
        {
            if(!ModelState.IsValid) {
               return View(auth);

            }
            var res = await unitOfWorkService.AccountService.Register(auth);
            if(res.Message== "there is user with this Email")
            {
                ModelState.AddModelError("", res.Message);
            }
            else if(res.Message!= "Register Succeeded")
                ModelState.AddModelError("", "Please, Try Again later");

           return RedirectToAction("Dashboard", "ClientDashboard");
        }
    }
}
