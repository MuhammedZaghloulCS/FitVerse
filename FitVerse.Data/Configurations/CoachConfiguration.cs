using FitVerse.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder.Property(c => c.About)
                   .HasMaxLength(1000);

            builder.Property(c => c.ImagePath)
                   .HasMaxLength(255);

            builder.HasMany(c => c.CoachSpecialties)
                   .WithOne(cs => cs.Coach)
                   .HasForeignKey(cs => cs.CoachId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Chats)
                   .WithOne(ch => ch.Coach)
                   .HasForeignKey(ch => ch.CoachId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.ClientSubscriptions)
                   .WithOne(cs => cs.Coach)
                   .HasForeignKey(cs => cs.CoachId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.CoachPackages)
                   .WithOne(cp => cp.Coach)
                   .HasForeignKey(cp => cp.CoachId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
