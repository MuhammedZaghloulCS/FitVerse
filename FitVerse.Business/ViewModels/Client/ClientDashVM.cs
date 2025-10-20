using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.Client
{
    public class ClientDashVM
    {
        public string Name { get; set; }
      
        public bool IsActive { get; set; }
        public string LastPaymentAgo { get; set; }
    }
}
