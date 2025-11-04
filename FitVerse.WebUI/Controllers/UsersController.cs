using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.ViewModels.Admin.User;
using FitVerse.Core.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FitVerse.Core.Models;
using System.Threading.Tasks;
using FitVerse.Core.ViewModels.Profile;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
                TempData["AddUserStatus"] = "error";
                TempData["AddUserMessage"] = message;
                ViewBag.ShowAddUserModal = true;
                return View("Index", newUser);
            }

            TempData["AddUserStatus"] = "success";
            TempData["AddUserMessage"] = "User added successfully!";
            return RedirectToAction("Index");
        }
        [HttpGet("profile/{UserName}")]
        public async Task<IActionResult> Profile(string UserName)
        {
            var res=await unitOFWorkService.UsersService.GetUserByUserNameAsync(UserName);
           string role=await unitOFWorkService.UsersService.showByRole(res.user);
            ProfileViewModel newUser = new ProfileViewModel() ;
            newUser.UserInfo =unitOFWorkService.UsersService.MapToGetAllUsersViewModel(res.user, role);
            
            return View("profile",newUser);
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(ProfileViewModel userImage)
        {
           await unitOFWorkService.UsersService.SaveOrUpdateImageInWWWRoot(userImage.UserInfo.Image, userImage.UserInfo.UserName);
            return RedirectToAction("Profile", new { userImage.UserInfo.UserName });

        }

        [HttpPost("UpdatePersonalInfo")]
        public async Task<IActionResult> UpdatePersonalInfo(GetAllUsersViewModel myUser)
        {
            if(!ModelState.IsValid)
                {
                return Json(new { Succeeded = false, Message = "Invalid data provided." });
            }

            var res=await unitOFWorkService.UsersService.UpdatePersonalInfoAsync(myUser);

            return Json(new { Succeeded = res.Item1, Message = res.Item2 });
        }
        [HttpPost("ChangeUserRole")]
        public async Task<IActionResult> ChangeUserRole(ChangeUserRoleViewModel userWithRole)
        {
            var res = await unitOFWorkService.UsersService.ChangeUserRoleAsync(userWithRole);
            return Json(new { Succeeded = res.Success, Message = res.Message });
        }
    
        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await unitOFWorkService.UsersService.DeleteUserAsync(id);
            
            if (result.Success)
            {
                return Json(new { 
                    success = true, 
                    message = result.Message 
                });
            }
            
            return Json(new { 
                success = false, 
                message = result.Message 
            });
        }
        [HttpPost("ChangePasswordByAdmin")]

        public async Task<IActionResult> ChangePasswordByAdmin( ProfileViewModel userPass)
        {
            var keysToKeep = new[]
     {
                    "ChangePasswordByAdmin.Password",
                    "ChangePasswordByAdmin.ConfirmPassword",
                    "UserInfo.UserName"
                };

            // Remove all other keys from ModelState
            foreach (var key in ModelState.Keys.Except(keysToKeep).ToList())
            {
                ModelState.Remove(key);
            }
            (bool Success, string Message) res = (false, "Change Password Failed, Please try again.");

            if (ModelState.IsValid)
            {
                res = await unitOFWorkService.UsersService
                    .ChangePasswordByAdminAsync(userPass.UserInfo.UserName, userPass.ChangePasswordByAdmin);
            }
            else
            {
                res = (false, "Invalid input data. Please check your form.");
            }

            return Json(new { Success = res.Success, Message = res.Message });
        }
        [HttpPost("ChangePasswordByUser")]

        public async Task<IActionResult> ChangePasswordByUser(ProfileViewModel userPass)
        {
           

            var keysToKeep = new[]
                {
                    "ChangePasswordByUser.OldPassword",
                    "ChangePasswordByUser.Password",
                    "ChangePasswordByUser.ConfirmPassword",
                    "UserInfo.UserName"
                };

            // Remove all other keys from ModelState
            foreach (var key in ModelState.Keys.Except(keysToKeep).ToList())
            {
                ModelState.Remove(key);
            }
            (bool Success, string Message) res = (false, "Change Password Failed, Please try again.");

            if (ModelState.IsValid)
            {
                res = await unitOFWorkService.UsersService
                    .ChangePasswordByUserAsync(userPass.UserInfo.UserName, userPass.ChangePasswordByUser);
            }
           else
            {
                res = (false, "Invalid input data. Please check your form.");
            }

            return Json(new { Success = res.Success, Message = res.Message });
        }

        //todo: Update Client Goals
        [HttpPost("UpdateClientGoals")]
        public IActionResult UpdateClientGoals(string userName, double? height, double? startWeight, string goal)
        {
            var clientInfo = new ClientViewModel
            {
                Height = height,
                StartWeight = startWeight,
                Goal = goal
            };

            var result = unitOFWorkService.ClientService.UpdateClientGoals(userName, clientInfo);
            return Json(new { result });
        }

    }

}
