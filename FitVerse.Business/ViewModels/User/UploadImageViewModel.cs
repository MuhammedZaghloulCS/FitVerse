using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.User
{
    public class UploadImageViewModel
    {
        public IFormFile Image { get; set; }
        public string UserName { get; set; }

    }

}
