using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }

        public int PackageId { get; set; }
        public virtual Package? Package { get; set; }
        public string ClientId { get; set; }
        public virtual Client? Client { get; set; }
    }
}
