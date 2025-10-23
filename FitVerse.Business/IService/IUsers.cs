using FitVerse.Core.Models;
using FitVerse.Core.ViewModels.Admin.User;
using FitVerse.Core.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.IService
{
    public interface IUsers
    {
        public  Task<List<GetAllUsersViewModel>> GetAllUsers();
        public  List<GetAllUsersViewModel> SearchBy(string nameOrEmail);
        public  List<GetAllUsersViewModel> showByRole(int id);
        Task<(bool Success, string Message)> AddUserAsync(AddUserByAdmin newUser);

        public Task<(bool Success, string Message)> UpdateUserAsync(AddUserByAdmin myUser);
        public Task<(bool Success, string Message)> DeleteUserAsync(string Id);



    }
}
