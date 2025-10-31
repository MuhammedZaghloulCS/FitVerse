using FitVerse.Core.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.Profile
{
    public class ProfileViewModel
    {
        public GetAllUsersViewModel UserInfo { get; set; }
        public UploadImageViewModel UserWithPhoto { get; set; }
        public ChangePasswordByAdmin ChangePasswordByAdmin { get; set; }
    }
}
