using FitVerse.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace FitVerse.Core.ViewModels.User
{
    public class GetAllUsersViewModel
    {
        private string _firstName;
        private string _lastName;

        public string Id { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
        public string FullName
        {
            get => $"{_firstName} {_lastName}".Trim();
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    var parts = value.Split(' ', 2);
                    _firstName = parts[0];
                    _lastName = parts.Length > 1 ? parts[1] : "";
                }
            }
        }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required")]
        [MinLength(3)]
        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        [MinLength(3)]
        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string UserName { get; set; }

        public string? ImagePath { get; set; }

        [Display(Name = "Profile Image")]
        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Please enter a valid Egyptian phone number.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Joined Date")]
        [DataType(DataType.Date)]
        public DateTime JoinedDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public UserGender? Gender { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(18, 55, ErrorMessage = "Age should be in range 18 to 55")]
        public int? Age { get; set; }

    }
}
