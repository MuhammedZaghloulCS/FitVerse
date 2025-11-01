using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.DailyLog
{
    public class ClientAddDailyLogInputVM
    {
    
        public double CurrentWeight { get; set; }
        public string ClientNotes { get; set; } = null!;
        public IFormFile? Photo { get; set; }
    }
}
