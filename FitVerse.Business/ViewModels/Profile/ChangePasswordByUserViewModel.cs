using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.Profile
{
    public class ChangePasswordByUserViewModel:ChangePasswordByAdminViewModel
    {

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
      ErrorMessage = "Password must have at least 1 uppercase, 1 lowercase, 1 digit, and be at least 8 characters long.")]
        public string OldPassword { get; set; }
    }
}
