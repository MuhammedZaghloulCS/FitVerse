using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.ViewModels.Admin.User;
using FitVerse.Core.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FitVerse.Core.Models;
using System.Threading.Tasks;
using FitVerse.Core.ViewModels.Profile;

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
        //[HttpPost("UpdateUser")]
        //public async Task<IActionResult> Profile(AddUserByAdmin myUser)
        //{
        //    ModelState.Remove("Id");
        //    ModelState.Remove("Password");
        //    ModelState.Remove("ConfirmPassword");

        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.ShowUpdateUserModal = true;
        //        return View("Index", myUser);
        //    }

        //    var (success, message) = await unitOFWorkService.UsersService.UpdateUserAsync(myUser);

        //    if (!success)
        //    {
        //        TempData["UpdateUserStatus"] = "error";
        //        TempData["UpdateUserMessage"] = message;
        //        ViewBag.ShowUpdateUserModal = true;
        //        return View("Index", myUser);
        //    }

        //    TempData["UpdateUserStatus"] = "success";
        //    TempData["UpdateUserMessage"] = "User updated successfully!";
        //    return RedirectToAction("Index");
        //}
        [HttpGet("DeleteUser/{Id}")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
           
            await unitOFWorkService.UsersService.DeleteUserAsync(Id);

            
            return RedirectToAction("Index");
        }
        [HttpPost("ChangePasswordByAdmin")]

        public async Task<IActionResult> ChangePasswordByAdmin( ProfileViewModel userPass)
        {
            #region make model valid
            ModelState.Remove("UserInfo.Id");
            ModelState.Remove("UserInfo.FirstName");
            ModelState.Remove("UserInfo.LastName");
            ModelState.Remove("UserInfo.FullName");
            ModelState.Remove("UserInfo.Email");
            ModelState.Remove("UserInfo.PhoneNumber");
            ModelState.Remove("UserInfo.Age");
            ModelState.Remove("UserInfo.Gender");
            ModelState.Remove("UserInfo.ImagePath");
            ModelState.Remove("UserInfo.Role");
            ModelState.Remove("UserInfo.Status");
            ModelState.Remove("UserInfo.JoinedDate");
            ModelState.Remove("UserWithPhoto");
            ModelState.Remove("UserWithPhoto.Image");
            ModelState.Remove("UserWithPhoto.UserName");
            #endregion 
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

    }

}
