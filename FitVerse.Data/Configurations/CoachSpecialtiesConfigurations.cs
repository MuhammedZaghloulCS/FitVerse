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
    public class CoachSpecialtiesConfigurations:IEntityTypeConfiguration<CoachSpecialties>
    {
        public void Configure(EntityTypeBuilder<CoachSpecialties> builder)
        {
            builder.HasKey(cs => new { cs.CoachId, cs.SpecialtyId });
            builder.HasOne(cs => cs.Coach)
                   .WithMany(c => c.CoachSpecialties)
                   .HasForeignKey(cs => cs.CoachId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(cs => cs.Specialty)
                   .WithMany(s => s.CoachSpecialties)
                   .HasForeignKey(cs => cs.SpecialtyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
