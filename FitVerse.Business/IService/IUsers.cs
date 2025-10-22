using FitVerse.Core.Models;
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
       
    }
}
