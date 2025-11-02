using AutoMapper;
using FitVerse.Core.Helpers;
using FitVerse.Core.IService;
using FitVerse.Core.Models;
using FitVerse.Core.ViewModels.Admin.Account;
using FitVerse.Core.ViewModels.Client;
using FitVerse.Data.Models;
using FitVerse.Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Service.Service
{

    public class AccountService : IAccountService
    {
        private UserManager<ApplicationUser> userManager;
        private IMapper mapper;
        private SignInManager<ApplicationUser> signInManager;
        private readonly IClientService clientService;

        //ctor
        public AccountService(UserManager<ApplicationUser> userManager, IMapper mapper, SignInManager<ApplicationUser> signInManager, IClientService clientService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.signInManager = signInManager;
            this.clientService = clientService;
        }
        public async Task<(bool Success, string Message, ApplicationUser User)> Login(Login auth)
        {
            var user = await userManager.FindByEmailAsync(auth.Email);
            if (user == null || !await userManager.CheckPasswordAsync(user, auth.Password))
            {
                return (Success: false, Message: "Email Or Password Is Invalid", null);
            }
            else
            {
                await signInManager.SignInAsync(user, auth.isPresisted);
            }
            return (Success: true, Message: "Login Succeeded", user);

        }
        public async Task<String> GetRole(ApplicationUser user)
        {
            var roles = await userManager.GetRolesAsync(user);

            return roles.FirstOrDefault();
        }



        public async Task<(bool Success, string Message)> Register(Register auth)
        {
            var oldUser = await userManager.FindByEmailAsync(auth.Email);
            if (oldUser != null)
                return (Success: false, Message: "there is user with this Email");

            var user = new ApplicationUser
            {
                UserName = auth.FirstName + auth.LastName + Guid.NewGuid().ToString().Substring(0, 6),
                FullName = auth.FirstName + " " + auth.LastName,
                Email = auth.Email,
                Status = "Active",
                PhoneNumber = auth.PhoneNumber,
                joinedDate = DateTime.Now
            };
            var res = await userManager.CreateAsync(user, auth.Password);
            if (res.Succeeded)
            {
                var client = new AddClientVM
                {
                    Id = user.Id,   
                    UserId = user.Id,
                };
                clientService.AddClient(client);
                await userManager.AddToRoleAsync(user, Statics.CLIENT);
                await signInManager.SignInAsync(user, false);
                return (Success: true, Message: "Register Succeeded");

            }

            return (Success: false, Message: "Please, Try Again later");
        }
    }
}
