using FitVerse.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public virtual Chat? Chat { get; set; }
        public string SenderId { get; set; }
        public virtual IdentityUser? Sender { get; set; }
        public string ReciverId { get; set; }
        public virtual IdentityUser? Reciver { get; set; }

        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }

    }

}
