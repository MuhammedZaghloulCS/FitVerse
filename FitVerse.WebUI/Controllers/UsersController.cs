using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.ViewModels.Admin.User;
using FitVerse.Core.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FitVerse.Core.Models;
using System.Threading.Tasks;

namespace FitVerse.Web.Controllers
{
    [Route("Admin/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUnitOFWorkService unitOFWorkService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(IUnitOFWorkService unitOFWorkService, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            this.unitOFWorkService = unitOFWorkService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            List<GetAllUsersViewModel> data = await unitOFWorkService.UsersService.GetAllUsers();
            return Json(new { data });
        }

        [HttpGet("SearchBy")]
        public IActionResult SearchBy(string nameOrEmail)
        {
            List<GetAllUsersViewModel> data = unitOFWorkService.UsersService.SearchBy(nameOrEmail);
            return Json(new { data });
        }
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(AddUserByAdmin newUser)
        {
            ModelState.Remove("Id");
            if (!ModelState.IsValid)
            {
                ViewBag.ShowAddUserModal = true;
                return View("Index", newUser);
            }

            var (success, message) = await unitOFWorkService.UsersService.AddUserAsync(newUser);

            if (!success)
            {
                ModelState.AddModelError("ModelOnly", message);
                ViewBag.ShowAddUserModal = true;
                return View("Index", newUser);
            }

            return RedirectToAction("Index");
        }
        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser(AddUserByAdmin myUser)
        {
            ModelState.Remove("Id");
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");
            if (!ModelState.IsValid)
            {
                ViewBag.ShowUpdateUserModal = true;
                return View("index", myUser);
            }
            var (success, message) = await unitOFWorkService.UsersService.UpdateUserAsync(myUser);

            if (!success)
            {
                ModelState.AddModelError("ModelOnly", message);
                ViewBag.ShowUpdateUserModal = true;
                return View("Index", myUser);
            }

            return RedirectToAction("Index");
        }
        [HttpGet("DeleteUser/{Id}")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
           
            await unitOFWorkService.UsersService.DeleteUserAsync(Id);

            
            return RedirectToAction("Index");
        }
    }

}
