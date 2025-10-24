using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.Client
{
    public class AddClientVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public IFormFile? ClientImageFile { get; set; }
        public string? ImagePath { get; set; }
        public Guid CoachId { get; set; }
    }
}
