using AutoMapper;
using FitVerse.Core.Helpers;
using FitVerse.Core.IService;
using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.Models;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Admin.User;
using FitVerse.Core.ViewModels.Profile;
using FitVerse.Core.ViewModels.User;
using FitVerse.Data.Models;
using FitVerse.Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
namespace FitVerse.Service.Service
{
    internal class UsersService : IUsersService
    {
        UserManager<ApplicationUser> userManager;
        IUnitOfWork UnitOfWork;
        IMapper mapper;
        public UsersService(UserManager<ApplicationUser> userManager, IMapper mapper ,IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.UnitOfWork = unitOfWork;
        }
        public async Task<List<GetAllUsersViewModel>> GetAllUsers()
        {
            // جلب كل المستخدمين مرة واحدة
            List<ApplicationUser> users = await userManager.Users.ToListAsync();


            var data = new List<GetAllUsersViewModel>();

            foreach (var user in users)
            {
                // جلب الـ role لكل مستخدم بشكل متتابع (مش متوازي)
                var roles = await userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault();

                var newUser = mapper.Map<GetAllUsersViewModel>(user);
                newUser.Id = user.Id;
                newUser.Role = role;
                newUser.PhoneNumber = user.PhoneNumber ?? "";

                data.Add(newUser);
            }

            return data;
        }

        public List<GetAllUsersViewModel> SearchBy(string nameOrEmail)
        {
            List<ApplicationUser> users = userManager.Users.Where(u => u.FullName.Contains(nameOrEmail) || u.Email.Contains(nameOrEmail)).ToList();
            return mapper.Map<List<GetAllUsersViewModel>>(users);

        }

        public async Task<String> showByRole(ApplicationUser user)
        {
            var res = await userManager.GetRolesAsync(user);
            return res.FirstOrDefault();

        }
        public async Task<(bool Success, string Message)> AddUserAsync(AddUserByAdmin newUser)
        {
            // Create the user object
            var user = new ApplicationUser
            {
                FullName = newUser.FirstName + " " + newUser.LastName,
                UserName = newUser.FirstName + newUser.LastName + Guid.NewGuid().ToString().Substring(0, 6),
                Email = newUser.Email,
                Status = newUser.Status,
                PhoneNumber = newUser.PhoneNumber,
                joinedDate = DateTime.Now,
                Gender = newUser.Gender

            };
            var oldUser = await userManager.FindByEmailAsync(newUser.Email);
            if (oldUser != null)
                return (false, "User Alreday created Before!");
            // Create user
            var result = await userManager.CreateAsync(user, newUser.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return (false, errors);
            }

            // Assign role
            await userManager.AddToRoleAsync(user, newUser.Role);
            //
            if(Statics.CLIENT==newUser.Role)
            {
                Client client = new Client
                {
                    
                    UserId = user.Id
                    
                };

                UnitOfWork.Clients.Add(client);
            }
            else if(Statics.COACH==newUser.Role)
            {
                Coach coach = new Coach
                {
                    
                    UserId = user.Id
                    
                };
                UnitOfWork.Coaches.Add(coach);
            }
            UnitOfWork.Complete();
                return (true, "User created successfully!");
        }
        public async Task<(bool Success, string Message)> UpdateUserAsync(AddUserByAdmin myUser)
        {
            // Find the user by Id
            var user = await userManager.FindByIdAsync(myUser.Id.ToString());
            if (user == null)
                return (false, "User not found");

            // Update basic info
            user.FullName = myUser.FirstName.Trim() + ' ' + myUser.LastName.Trim();
            user.Email = myUser.Email;
            user.PhoneNumber = myUser.PhoneNumber;
            user.Status = myUser.Status;

            // Update user
            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return (false, "Failed to update user, this data may be invalid or this Email used before.");

            // Update roles if changed
            var currentRoles = await userManager.GetRolesAsync(user);
            if (!currentRoles.Contains(myUser.Role))
            {
                // Remove all old roles and add the new one
                await userManager.RemoveFromRolesAsync(user, currentRoles);
                await userManager.AddToRoleAsync(user, myUser.Role);
            }



            return (true, "User updated successfully");
        }

        public async Task<(bool Success, string Message)> DeleteUserAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return (false, "User not found");

            var res = await userManager.DeleteAsync(user);
            if (res.Succeeded)
            {
                return (true, "User Deleted successfully");
            }
            return (false, "Try Again");
        }

        public async Task<(bool Success, string Message, ApplicationUser user)> GetUserByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return (success: false, Message: "User Not Found", user: null);
            }
            return (true, "User Found", user);
        }
        public async Task<(bool Success, string Message, ApplicationUser user)> GetUserByUserNameAsync(string UserName)
        {
            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                return (success: false, Message: "User Not Found", user: null);
            }
            return (true, "User Found", user);
        }
        public async Task<(bool Success, string Message, ApplicationUser user)> GetUserById(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return (success: false, Message: "User Not Found", user: null);
            }
            return (true, "User Found", user);
        }
        public GetAllUsersViewModel MapToGetAllUsersViewModel(ApplicationUser user, string role)
        {
            var viewModel = mapper.Map<GetAllUsersViewModel>(user);
            viewModel.Id = user.Id;
            viewModel.Role = role;
            viewModel.PhoneNumber = user.PhoneNumber;
            viewModel.Email = user.Email;
            viewModel.Age=user.Age;
            viewModel.Gender= user.Gender;

            return viewModel;
        }


        public async Task<string> SaveImageInWWWRoot(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                string fileName = Path.GetFileName(image.FileName).Insert(0, DateTime.Now.ToString("yyyymmddHHMMSSFF"));

                string uploadPathOnly = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");

                if (!Directory.Exists(uploadPathOnly))
                {
                    Directory.CreateDirectory(uploadPathOnly);
                }

                string filePath = Path.Combine(uploadPathOnly, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                return $"/img/{fileName}";
            }
            return null;

        }
        public async Task<string> SaveOrUpdateImageInWWWRoot(IFormFile image, string UserName)
        {
            if (image != null && image.Length > 0)
            {
                string fileName = Path.GetFileName(image.FileName).Insert(0, DateTime.Now.ToString("yyyymmddHHMMSSFF"));

                string uploadPathOnly = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");

                if (!Directory.Exists(uploadPathOnly))
                {
                    Directory.CreateDirectory(uploadPathOnly);
                }

                string filePath = Path.Combine(uploadPathOnly, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                var user = await userManager.FindByNameAsync(UserName);

                var lastImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", user.ImagePath?.TrimStart('/').Replace('/', Path.DirectorySeparatorChar) ?? "");


                if (System.IO.File.Exists(lastImage))
                {

                    System.IO.File.Delete(lastImage);
                }
                user.ImagePath = $"/img/{fileName}";
                await userManager.UpdateAsync(user);
                await userManager.UpdateAsync(user);
                return $"/img/{fileName}";
            }
            return null;

        }

        public async Task<(bool, string)> UpdatePersonalInfoAsync(GetAllUsersViewModel user)
        {
            var foundUser = await userManager.FindByEmailAsync(user.Email);
            var appUser = await userManager.FindByNameAsync(user.UserName);

            if (foundUser != null && (foundUser.UserName != appUser.UserName))
                return (Success: false, Message: "The Email is Already Used");

            var currentRoles = await userManager.GetRolesAsync(appUser);


            appUser.PhoneNumber = user.PhoneNumber;
            appUser.Email = user.Email;
            appUser.FullName = user.FullName;
            appUser.Age = user.Age;
            appUser.Gender = user.Gender;

            var result = await userManager.UpdateAsync(appUser);
            return (Success: true, Message: "Personal info updated successfully!");

        }

        public async Task<(bool Success, string Message)> ChangeUserRoleAsync(ChangeUserRoleViewModel userWithRole)
        {
            var user = await userManager.FindByNameAsync(userWithRole.UserName);
            if (user == null)
                return (false, "User not found");
            var currentRoles = await userManager.GetRolesAsync(user);
            if (!currentRoles.Contains(userWithRole.Role))
            {
                // Remove all old roles and add the new one
                await userManager.AddToRoleAsync(user, userWithRole.Role);
                await userManager.RemoveFromRolesAsync(user, currentRoles);
            }
            return (true, "User role updated successfully");

        }
        public async Task<(bool Success, string Message)> ChangePasswordByAdminAsync(string UserName, ChangePasswordByAdminViewModel passwords)
        {
            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
                return (false, "User not found");
            
            await userManager.RemovePasswordAsync(user);
            await userManager.AddPasswordAsync(user, passwords.Password);
               

            return (true, "User Password updated successfully");

        }
        public async Task<(bool Success, string Message)> ChangePasswordByUserAsync(string UserName, ChangePasswordByUserViewModel passwords)
        {
            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
                return (false, "User not found");
            
            var result= await userManager.ChangePasswordAsync(user, passwords.OldPassword, passwords.Password);
            if (!result.Succeeded)
            {
                return (false, "Updating password failed, please try again");

            }


            return (true, "User Password updated successfully");

        }

    }

}
