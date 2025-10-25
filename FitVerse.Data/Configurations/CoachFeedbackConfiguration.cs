using FitVerse.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Configurations
{
    public class CoachFeedbackConfiguration : IEntityTypeConfiguration<CoachFeedback>
    {
        public void Configure(EntityTypeBuilder<CoachFeedback> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Rate)
                   .IsRequired();

            builder.Property(f => f.Comments)
                   .HasMaxLength(1000);

            builder.HasOne(f => f.Client)
                   .WithOne(c => c.CoachFeedback)
                   .HasForeignKey<CoachFeedback>(f => f.ClientId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(f => f.Coach)
                   .WithMany(c => c.CoachFeedbacks)
                   .HasForeignKey(f => f.CoachId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
