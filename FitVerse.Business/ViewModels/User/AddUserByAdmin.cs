using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.Admin.User
{
    public class AddUserByAdmin
    {
        public string Id { get; set; }
        [Display(Name ="First Name")]
        [Required(ErrorMessage ="First name is required")]
        [MinLength(3)]
        public string FirstName { get; set; }
        [Display(Name ="Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        [MinLength(3)]
        public string LastName { get; set; }



        [Required(ErrorMessage ="Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
      ErrorMessage = "Password must have at least 1 uppercase, 1 lowercase, 1 digit, and be at least 8 characters long.")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Phone")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Please enter a valid Egyptian phone number.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Account Status is required")]

        public string Status { get; set; }


    }
}
