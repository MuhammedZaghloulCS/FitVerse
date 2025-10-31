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
                      Icon = "fa-solid fa-dumbbell",
                      Color = "#007bff"
                  },
    new Specialty
    {
        Id = 2,
        Name = "Cardio & HIIT",
        Description = "Cardiovascular fitness",
        Icon = "fa-solid fa-heartbeat",
        Color = "#dc3545"
    },
    new Specialty
    {
        Id = 3,
        Name = "Flexibility & Yoga",
        Description = "Mobility and stretching",
        Icon = "fa-solid fa-person-praying",
        Color = "#20c997"
    },
    new Specialty
    {
        Id = 4,
        Name = "CrossFit",
        Description = "High-intensity functional training",
        Icon = "fa-solid fa-bolt",
        Color = "#fd7e14"
    },
    new Specialty
    {
        Id = 5,
        Name = "Boxing & MMA",
        Description = "Combat sports training",
        Icon = "fa-solid fa-hand-fist",
        Color = "#6610f2"
    },
    new Specialty
    {
        Id = 6,
        Name = "Bodybuilding",
        Description = "Muscle hypertrophy focus",
        Icon = "fa-solid fa-trophy",
        Color = "#ffc107"
    },
    new Specialty
    {
        Id = 7,
        Name = "Running & Endurance",
        Description = "Distance and stamina",
        Icon = "fa-solid fa-person-running",
        Color = "#198754"
    },
    new Specialty
    {
        Id = 8,
        Name = "Weight Loss",
        Description = "Fat loss and nutrition",
        Icon = "fa-solid fa-scale-balanced",
        Color = "#0dcaf0"
    }
            );
        }
    }
}
