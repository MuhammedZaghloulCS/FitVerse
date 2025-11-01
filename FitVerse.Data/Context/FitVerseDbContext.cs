using FitVerse.Core.Models;
using FitVerse.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace FitVerse.Data.Context
{
    public class FitVerseDbContext :IdentityDbContext<ApplicationUser>
    {
        public FitVerseDbContext(DbContextOptions<FitVerseDbContext> options)
            : base(options)
        {

        }
  
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Anatomy> Anatomies { get; set; }
        public DbSet<CoachSpecialties> CoachSpecialties { get; set; }
        public DbSet<CoachFeedback> CoachFeedbacks { get; set; }
        public DbSet<DietPlan> DietPlans { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ExercisePlan> ExercisePlans { get; set; }
        public DbSet<ExercisePlanDetail> ExercisePlanDetails { get; set; }
        public DbSet<Muscle>Muscles { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Specialty> Specialties { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coach>().HasData(
    new Coach
    {
        Id = "C1",
        Name = "John Smith",
        ExperienceYears = 8,
        About = "Expert in Strength and Conditioning",
        ImagePath = "/images/coaches/john.jpg",
        IsActive = true
    },
    new Coach
    {
        Id = "C2",
        Name = "Sarah Johnson",
        ExperienceYears = 6,
        About = "Cardio and endurance specialist with personalized HIIT plans.",
        ImagePath = "/images/coaches/sarah.jpg",
        IsActive = true
    },
    new Coach
    {
        Id = "C3",
        Name = "Michael Lee",
        ExperienceYears = 7,
        About = "Yoga and mobility instructor focused on flexibility and wellness.",
        ImagePath = "/images/coaches/michael.jpg",
        IsActive = true
    },
    new Coach
    {
        Id = "C4",
        Name = "Chris Evans",
        ExperienceYears = 5,
        About = "CrossFit certified coach delivering high-intensity programs.",
        ImagePath = "/images/coaches/chris.jpg",
        IsActive = true
    },
    new Coach
    {
        Id = "C5",
        Name = "Amanda Davis",
        ExperienceYears = 4,
        About = "Boxing and MMA trainer with focus on endurance and strength.",
        ImagePath = "/images/coaches/amanda.jpg",
        IsActive = true
    },
    new Coach
    {
        Id = "C6",
        Name = "Robert Wilson",
        ExperienceYears = 10,
        About = "Professional bodybuilder and muscle growth expert.",
        ImagePath = "/images/coaches/robert.jpg",
        IsActive = true
    },
    new Coach
    {
        Id = "C7",
        Name = "Emily Clark",
        ExperienceYears = 5,
        About = "Running and endurance coach with marathon training expertise.",
        ImagePath = "/images/coaches/emily.jpg",
        IsActive = true
    },
    new Coach
    {
        Id = "C8",
        Name = "David Harris",
        ExperienceYears = 6,
        About = "Nutrition and weight loss expert with balanced diet programs.",
        ImagePath = "/images/coaches/david.jpg",
        IsActive = true
    }
);
            modelBuilder.Entity<CoachSpecialties>().HasData(
                new CoachSpecialties { CoachId = "C1", SpecialtyId = 1 },
                new CoachSpecialties { CoachId = "C2", SpecialtyId = 2 },
                new CoachSpecialties { CoachId = "C3", SpecialtyId = 3 },
                new CoachSpecialties { CoachId = "C4", SpecialtyId = 4 },
                new CoachSpecialties { CoachId = "C5", SpecialtyId = 5 },
                new CoachSpecialties { CoachId = "C6", SpecialtyId = 6 },
                new CoachSpecialties { CoachId = "C7", SpecialtyId = 7 },
                new CoachSpecialties { CoachId = "C8", SpecialtyId = 8 }
            );

            modelBuilder.Entity<Specialty>().HasData(
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
