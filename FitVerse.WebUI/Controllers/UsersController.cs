using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.Models;
using FitVerse.Core.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FitVerse.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUnitOFWorkService unitOFWorkService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(IUnitOFWorkService unitOFWorkService, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            this.unitOFWorkService = unitOFWorkService;
      
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllUsers()
        {

            List<GetAllUsersViewModel> data = await unitOFWorkService.UsersService.GetAllUsers();

            return Json(new { data });
        }
        public IActionResult SearchBy(string nameOrEmail)
        {
            List<GetAllUsersViewModel> data = unitOFWorkService.UsersService.SearchBy(nameOrEmail);
            return Json(new { data });
        }

    }

}