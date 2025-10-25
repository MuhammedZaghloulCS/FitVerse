using FitVerse.Core.IService;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Service
{
    //SERVICE THAT RETURN IMAGE PATH AFTER SAVING IT IN wwwroot/Images FOLDER 
    public class ImageHandleService : IImageHandleService
    {
        public string? SaveImage(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return null;

            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"/Images/{fileName}";
        }
    }
}
