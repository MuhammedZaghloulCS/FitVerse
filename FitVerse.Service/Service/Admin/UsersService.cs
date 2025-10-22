using AutoMapper;
using FitVerse.Core.IService;
using FitVerse.Core.Models;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace FitVerse.Service.Service
{
    internal class UsersService : IUsers
    {
        UserManager<ApplicationUser> userManager;
        
        IMapper mapper;
        public UsersService(UserManager<ApplicationUser> userManager,IMapper mapper)
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
                newUser.Role = role;
                newUser.Id = user.Id.Substring(0,8);

                data.Add(newUser);
            }

            return data;
        }

        public List<GetAllUsersViewModel> SearchBy(string nameOrEmail)
        {
            List<ApplicationUser> users = userManager.Users.Where(u=>u.UserName.Contains(nameOrEmail)||u.Email.Contains(nameOrEmail)).ToList();
            return mapper.Map<List<GetAllUsersViewModel>>(users);

        }

        public List<GetAllUsersViewModel> showByRole(int id)
        {
            throw new NotImplementedException();
        }
    } 
    
}
