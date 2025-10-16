using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Sessions { get; set; }

        public Guid? CoachId { get; set; }
        public virtual Coach? Coach { get; set; }
        public virtual ICollection<Client> Clients { get; set; }=new HashSet<Client>();
        public virtual ICollection<Payment> Payments { get; set; }=new HashSet<Payment>();

    }

}
