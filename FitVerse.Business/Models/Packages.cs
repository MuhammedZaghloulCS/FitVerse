using FitVerse.Core.Models;
using System;
using System.Collections.Generic;

namespace FitVerse.Data.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public int Sessions { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<CoachPackage> CoachPackages { get; set; } = new List<CoachPackage>();
        public virtual ICollection<ClientSubscription> ClientSubscriptions { get; set; } = new List<ClientSubscription>();
        public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
    }
}
