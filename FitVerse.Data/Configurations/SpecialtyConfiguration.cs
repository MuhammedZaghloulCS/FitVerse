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
    internal class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.HasData(
                  new Specialty
                  {
                      Id = 1,
                      Name = "Strength Training",
                      Description = "Building muscle and power",

                  },
    new Specialty
    {
        Id = 2,
        Name = "Cardio & HIIT",
        Description = "Cardiovascular fitness",

    },
    new Specialty
    {
        Id = 3,
        Name = "Flexibility & Yoga",
        Description = "Mobility and stretching",

    },
    new Specialty
    {
        Id = 4,
        Name = "CrossFit",
        Description = "High-intensity functional training",

    },
    new Specialty
    {
        Id = 5,
        Name = "Boxing & MMA",
        Description = "Combat sports training",

    },
    new Specialty
    {
        Id = 6,
        Name = "Bodybuilding",
        Description = "Muscle hypertrophy focus",

    },
    new Specialty
    {
        Id = 7,
        Name = "Running & Endurance",
        Description = "Distance and stamina",

    },
    new Specialty
    {
        Id = 8,
        Name = "Weight Loss",
        Description = "Fat loss and nutrition",

    }
            );
        }
    }
}
