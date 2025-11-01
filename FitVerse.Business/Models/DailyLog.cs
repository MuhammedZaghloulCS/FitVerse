using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitVerse.Data.Models
{
    public class DailyLog
    {
        public int Id { get; set; }

        public string ClientId { get; set; } = null!;
        public virtual Client Client { get; set; } = null!;

        public string CoachId { get; set; } = null!;
        public virtual Coach Coach { get; set; } = null!;

        public DateTime LogDate { get; set; } = DateTime.UtcNow;

        public double CurrentWeight { get; set; }           
        public string? PhotoPath { get; set; }              

        public string ClientNotes { get; set; } = null!;

        public string? CoachFeedback { get; set; }

        public int? CoachRating { get; set; }

        public bool IsReviewed { get; set; } = false;
    }
}

