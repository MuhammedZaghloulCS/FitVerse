using FitVerse.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitVerse.Data.Configurations
{
    public class CoachPackageConfiguration : IEntityTypeConfiguration<CoachPackage>
    {
        public void Configure(EntityTypeBuilder<CoachPackage> builder)
        {
            builder.HasKey(cp => new { cp.CoachId, cp.PackageId });

            builder.HasOne(cp => cp.Coach)
                   .WithMany(c => c.CoachPackages)
                   .HasForeignKey(cp => cp.CoachId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cp => cp.Package)
                   .WithMany(p => p.CoachPackages)
                   .HasForeignKey(cp => cp.PackageId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
