using FitVerse.Core.Helpers;
using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.Models;
using FitVerse.Core.ViewModels.Admin.Account;
using FitVerse.Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FitVerse.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOFWorkService unitOfWorkService;
        private readonly ILogger<AccountController> logger;

        public AccountController(IUnitOFWorkService unitOfWorkService, ILogger<AccountController> logger)
        {
            this.unitOfWorkService = unitOfWorkService;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            // Clear any existing session data for security
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login auth)
        {
            try
            {
                // Validate input model
                if (!ModelState.IsValid)
                {
                    logger.LogWarning($"Login attempt with invalid model for email: {auth.Email}");
                    return View(auth);
                }

                // Attempt login with optimized authentication
                var result = await unitOfWorkService.AccountService.LoginOptimized(auth);
                
                if (!result.Success)
                {
                    logger.LogWarning($"Failed login attempt for email: {auth.Email}");
                    ModelState.AddModelError("", result.Message ?? "Invalid email or password");
                    return View(auth);
                }

                // Get user role in a single query
                var role = await unitOfWorkService.AccountService.GetRoleOptimized(result.User);
                
                // Direct redirect based on role - no redundant calls
                if (role == Statics.ADMIN)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (role == Statics.COACH)
                {
                    return RedirectToAction("Dashboard", "Coach");
                }
                else if (role == Statics.CLIENT)
                {
                    return RedirectToAction("Dashboard", "ClientDashboard");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error during login for email: {auth.Email}");
                ModelState.AddModelError("", "An error occurred during login. Please try again.");
                return View(auth);
            }
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
