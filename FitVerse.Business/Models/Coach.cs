using BuilderGenerator;
using FitVerse.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitVerse.Data.Models
{
    [BuilderFor(typeof(Coach))]
    public partial class CoachBuilder { }

    public class Coach
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); // يتم إنشاء Id تلقائي
        public string Name { get; set; } = null!;
        public int ExperienceYears { get; set; }
        public string About { get; set; } = null!;
        public string? ImagePath { get; set; }
        public bool IsActive { get; set; }

        public string? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<CoachSpecialties> CoachSpecialties { get; set; } = new HashSet<CoachSpecialties>();
        public virtual ICollection<CoachPackage> CoachPackages { get; set; } = new List<CoachPackage>();
        public virtual ICollection<ClientSubscription> ClientSubscriptions { get; set; } = new List<ClientSubscription>();

        public virtual ICollection<ExercisePlan> ExercisePlans { get; set; } = new HashSet<ExercisePlan>();
        public virtual ICollection<DietPlan> DietPlans { get; set; } = new HashSet<DietPlan>();
        public virtual ICollection<CoachFeedback> CoachFeedbacks { get; set; } = new HashSet<CoachFeedback>();
        public virtual ICollection<Chat> Chats { get; set; } = new HashSet<Chat>();
    }
}
