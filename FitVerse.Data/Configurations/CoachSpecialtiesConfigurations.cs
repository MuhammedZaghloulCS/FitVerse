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

           // builder.HasData(
           //     new Specialty
           //     {
           //         Id = 1,
           //         Name = "Strength Training",
           //         Description = "Building muscle and power",
           //         Icon = "fa-solid fa-dumbbell",
           //         Color = "#007bff"
           //     },
           //     new Specialty
           //     {
           //         Id = 2,
           //         Name = "Cardio & HIIT",
           //         Description = "Cardiovascular fitness",
           //         Icon = "fa-solid fa-heartbeat",
           //         Color = "#dc3545"
           //     },
           //     new Specialty
           //     {
           //         Id = 3,
           //         Name = "Flexibility & Yoga",
           //         Description = "Mobility and stretching",
           //         Icon = "fa-solid fa-person-praying",
           //         Color = "#20c997"
           //     },
           //     new Specialty
           //     {
           //         Id = 4,
           //         Name = "CrossFit",
           //         Description = "High-intensity functional training",
           //         Icon = "fa-solid fa-bolt",
           //         Color = "#fd7e14"
           //     },
           //     new Specialty
           //     {
           //         Id = 5,
           //         Name = "Boxing & MMA",
           //         Description = "Combat sports training",
           //         Icon = "fa-solid fa-hand-fist",
           //         Color = "#6610f2"
           //     },
           //     new Specialty
           //     {
           //         Id = 6,
           //         Name = "Bodybuilding",
           //         Description = "Muscle hypertrophy focus",
           //         Icon = "fa-solid fa-trophy",
           //         Color = "#ffc107"
           //     },
           //     new Specialty
           //     {
           //         Id = 7,
           //         Name = "Running & Endurance",
           //         Description = "Distance and stamina",
           //         Icon = "fa-solid fa-person-running",
           //         Color = "#198754"
           //     },
           //     new Specialty
           //     {
           //         Id = 8,
           //         Name = "Weight Loss",
           //         Description = "Fat loss and nutrition",
           //         Icon = "fa-solid fa-scale-balanced",
           //         Color = "#0dcaf0"
           //     }
           // );
           // builder.HasData(
           //    new CoachSpecialties { CoachId = "C1", SpecialtyId = 1 },
           //    new CoachSpecialties { CoachId = "C2", SpecialtyId = 2 },
           //    new CoachSpecialties { CoachId = "C3", SpecialtyId = 3 },
           //    new CoachSpecialties { CoachId = "C4", SpecialtyId = 4 },
           //    new CoachSpecialties { CoachId = "C5", SpecialtyId = 5 },
           //    new CoachSpecialties { CoachId = "C6", SpecialtyId = 6 },
           //    new CoachSpecialties { CoachId = "C7", SpecialtyId = 7 },
           //    new CoachSpecialties { CoachId = "C8", SpecialtyId = 8 }
           //);
        }
    }
}
