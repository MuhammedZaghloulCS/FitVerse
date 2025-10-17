using FitVerse.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.Coach
{
    public class AddCoachVM
    {
        public string Name { get; set; }
        public string Title { get; set; }

        public string About { get; set; }
        public string CoachImagePath { get; set; }
        public IFormFile CoachImageFile { get; set; }
        public bool IsActive { get; set; }
        public Guid? UserId { get; set; } 

    }
}
