using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Models
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }
        public double StartWeight { get; set; }
        public string Goal { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }



        public Guid UserId { get; set; }
        public virtual IdentityUser User { get; set; }

        public Guid CoachId { get; set; }
        public virtual Coach? Coach { get; set; }

        public int? PackageId { get; set; }
        public virtual Package? Package { get; set; }
        public virtual CoachFeedback? CoachFeedback { get; set; }

        public virtual ICollection<Payment>? Payments { get; set; } = new HashSet<Payment>();
        public virtual ICollection<Chat>? Chats { get; set; } = new HashSet<Chat>();
        public virtual ICollection<ExercisePlan>? ExercisePlans { get; set; } = new HashSet<ExercisePlan>();
        public virtual ICollection<DietPlan>? DietPlans { get; set; } = new HashSet<DietPlan>();

    }

}
