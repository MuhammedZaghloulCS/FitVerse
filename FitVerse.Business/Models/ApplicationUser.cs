using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.Models
{
    public class ApplicationUser:IdentityUser
    {
       public DateTime joinedDate { get; set; }
       public string Status { get; set; }
    }
}
