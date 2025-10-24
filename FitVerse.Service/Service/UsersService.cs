using AutoMapper;
using FitVerse.Core.IService;
using FitVerse.Core.Models;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Admin.User;
using FitVerse.Core.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace FitVerse.Service.Service
{
    internal class UsersService : IUsersService
    {
        UserManager<ApplicationUser> userManager;

        IMapper mapper;
        public UsersService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
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
            List<ApplicationUser> users = userManager.Users.Where(u => u.UserName.Contains(nameOrEmail) || u.Email.Contains(nameOrEmail)).ToList();
            return mapper.Map<List<GetAllUsersViewModel>>(users);

        }

        public List<GetAllUsersViewModel> showByRole(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<(bool Success, string Message)> AddUserAsync(AddUserByAdmin newUser)
        {
            // Create the user object
            var user = new ApplicationUser
            {
                UserName = newUser.FirstName + " " + newUser.LastName,
                Email = newUser.Email,
                Status = newUser.Status,
                PhoneNumber = newUser.PhoneNumber,
                joinedDate = DateTime.Now
            };
            var oldUser = await userManager.FindByEmailAsync(newUser.Email);
            if(oldUser!=null)
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

            return (true, "User created successfully!");
        }
        public async Task<(bool Success, string Message)> UpdateUserAsync(AddUserByAdmin myUser)
        {
            // Find the user by Id
            var user = await userManager.FindByIdAsync(myUser.Id.ToString());
            if (user == null)
                return (false, "User not found");

            // Update basic info
            user.UserName = myUser.FirstName.Trim()+' '+myUser.LastName.Trim();
            user.Email = myUser.Email;
            user.PhoneNumber = myUser.PhoneNumber;
            user.Status = myUser.Status;

            // Update user
            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return (false, "Failed to update user");

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

           var res=await userManager.DeleteAsync(user);
            if (res.Succeeded)
            {
                return (true, "User Deleted successfully");
            }
            return (false, "Try Again");
        }
    }

}
