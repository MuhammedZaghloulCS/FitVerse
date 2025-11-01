using FitVerse.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitVerse.Data.Models
{
    public class Client
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public double? Height { get; set; }
        public double? StartWeight { get; set; }
        public string? Goal { get; set; } 
        

        public string? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } 

        public virtual ICollection<ClientSubscription> ClientSubscriptions { get; set; } = new List<ClientSubscription>();
        public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
        public virtual ICollection<Chat> Chats { get; set; } = new HashSet<Chat>();
        public virtual ICollection<ExercisePlan> ExercisePlans { get; set; } = new HashSet<ExercisePlan>();
        public virtual ICollection<DietPlan> DietPlans { get; set; } = new HashSet<DietPlan>();
        public virtual CoachFeedback? CoachFeedback { get; set; }
        public virtual ICollection<DailyLog> DailyLogs { get; set; } = new HashSet<DailyLog>();

    }
}
