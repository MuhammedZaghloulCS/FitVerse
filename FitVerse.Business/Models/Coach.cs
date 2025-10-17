using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Models
{
    public class Coach
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        public string About { get; set; }
        public string? ImagePath { get; set; }
        public bool IsActive { get; set; }

        public Guid? UserId { get; set; }//لازم بعدين يتيغر ان ميقبلش null
        public IdentityUser User { get; set; }
        public virtual ICollection<CoachSpecialties>? CoachSpecialties { get; set; }=new HashSet<CoachSpecialties>();
        public virtual ICollection<Package>? Packages { get; set; }=new HashSet<Package>();
        public virtual ICollection<Client>? Clients { get; set; }=new HashSet<Client>();
        public virtual ICollection<ExercisePlan>? ExercisePlans { get; set; }=new HashSet<ExercisePlan>();
        public virtual ICollection<DietPlan>? DietPlans { get; set; }=new HashSet<DietPlan>();
        public virtual ICollection<CoachFeedback>? CoachFeedbacks { get; set; }=new HashSet<CoachFeedback>();
        public virtual ICollection<Chat>? Chats { get; set; } = new HashSet<Chat>();
    }

}

