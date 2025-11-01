using FitVerse.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitVerse.Data.Models
{
    public class Client
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); // يتم إنشاء Id تلقائي
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public double Height { get; set; }
        public double StartWeight { get; set; }
        public string Goal { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Image { get; set; } = null!;
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<ClientSubscription> ClientSubscriptions { get; set; } = new List<ClientSubscription>();
        public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
        public virtual ICollection<Chat> Chats { get; set; } = new HashSet<Chat>();
        public virtual ICollection<ExercisePlan> ExercisePlans { get; set; } = new HashSet<ExercisePlan>();
        public virtual ICollection<DietPlan> DietPlans { get; set; } = new HashSet<DietPlan>();
        public virtual CoachFeedback? CoachFeedback { get; set; }
        public virtual ICollection<DailyLog> DailyLogs { get; set; } = new HashSet<DailyLog>();

    }
}
