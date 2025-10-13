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
    public class CoachConfiguration : IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Title)
                   .HasMaxLength(100);

            builder.Property(c => c.About)
                   .HasMaxLength(1000);

            builder.Property(c => c.ImagePath)
                   .HasMaxLength(255);

            builder.HasMany(c => c.Clients)
                   .WithOne(c => c.Coach)
                   .HasForeignKey(c => c.CoachId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Packages)
                   .WithOne(p => p.Coach)
                   .HasForeignKey(p => p.CoachId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.CoachSpecialties)
                   .WithOne(cs => cs.Coach)
                   .HasForeignKey(cs => cs.CoachId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Chats)
                   .WithOne(ch => ch.Coach)
                   .HasForeignKey(ch => ch.CoachId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
