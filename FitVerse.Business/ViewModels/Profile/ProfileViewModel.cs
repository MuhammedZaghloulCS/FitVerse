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
        public ChangePasswordByAdminViewModel ChangePasswordByAdmin { get; set; }
        public ChangePasswordByUserViewModel ChangePasswordByUser { get; set; }
        public CoachViewModel coachProfessionalInfo { get; set; }

        public ClientViewModel clientPhysicalInfo { get; set; }
    }
}
