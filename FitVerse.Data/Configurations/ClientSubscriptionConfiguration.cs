using FitVerse.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitVerse.Data.Configurations
{
    public class ClientSubscriptionConfiguration : IEntityTypeConfiguration<ClientSubscription>
    {
        public void Configure(EntityTypeBuilder<ClientSubscription> builder)
        {
            builder.HasKey(cs => cs.Id);

            builder.Property(cs => cs.PriceAtPurchase)
                   .HasColumnType("decimal(10,2)");

            builder.Property(cs => cs.Status)
                   .HasMaxLength(50)
                   .HasDefaultValue("Active");

            builder.HasOne(cs => cs.Client)
                   .WithMany(c => c.ClientSubscriptions)
                   .HasForeignKey(cs => cs.ClientId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cs => cs.Coach)
                   .WithMany(c => c.ClientSubscriptions)
                   .HasForeignKey(cs => cs.CoachId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cs => cs.Package)
                   .WithMany(p => p.ClientSubscriptions)
                   .HasForeignKey(cs => cs.PackageId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
