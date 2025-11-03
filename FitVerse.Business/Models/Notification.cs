using FitVerse.Core.Enums;
using FitVerse.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public string ReciverId { get; set; }

        public virtual ApplicationUser? Reciver { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int RefId { get; set; }
        public virtual NotificationType Type { get; set; }
        public bool IsRead { get; set; } = false;
    }


}
