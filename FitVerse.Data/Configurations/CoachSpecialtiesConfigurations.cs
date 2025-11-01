using FitVerse.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Configurations
{
    public class CoachSpecialtiesConfigurations : IEntityTypeConfiguration<CoachSpecialties>
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

            builder.HasData(
                new CoachSpecialties { CoachId = "C1", SpecialtyId = 1 },
                new CoachSpecialties { CoachId = "C2", SpecialtyId = 2 },
                new CoachSpecialties { CoachId = "C3", SpecialtyId = 3 },
                new CoachSpecialties { CoachId = "C4", SpecialtyId = 4 },
                new CoachSpecialties { CoachId = "C5", SpecialtyId = 5 },
                new CoachSpecialties { CoachId = "C6", SpecialtyId = 6 },
                new CoachSpecialties { CoachId = "C7", SpecialtyId = 7 },
                new CoachSpecialties { CoachId = "C8", SpecialtyId = 8 }
            );
        }
    }
}
