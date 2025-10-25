using FitVerse.Core.Models;
using FitVerse.Core.ViewModels.Admin.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.IService
{
    public interface IAccountService:IService
    {
        public Task<(bool Success, string Message,ApplicationUser User)> Login(Login auth);
        public Task<(bool Success, string Message)> Register(Register auth);
        public  Task<String> GetRole(ApplicationUser user);

    }
}
