using FitVerse.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Context
{
    public class FitVerseDbContext :IdentityDbContext
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
            modelBuilder.Entity<Anatomy>().HasData(
        new Anatomy { Id = 1, Name = "Upper Body" },
        new Anatomy { Id = 2, Name = "Lower Body" },
        new Anatomy { Id = 3, Name = "Core" });

            var coachId = new Guid("11111111-1111-1111-1111-111111111111");
            var clientId = new Guid("22222222-2222-2222-2222-222222222222");


            // 💪 Muscles
            modelBuilder.Entity<Muscle>().HasData(
                new Muscle { Id = 1, Name = "Biceps", AnatomyId = 1 },
                new Muscle { Id = 2, Name = "Triceps", AnatomyId = 1 },
                new Muscle { Id = 3, Name = "Quadriceps", AnatomyId = 2 },
                new Muscle { Id = 4, Name = "Abs", AnatomyId = 3 }
            );

            // 🏋️ Equipments
            modelBuilder.Entity<Equipment>().HasData(
                new Equipment { Id = 1, Name = "Dumbbell" },
                new Equipment { Id = 2, Name = "Barbell" },
                new Equipment { Id = 3, Name = "Machine" },
                new Equipment { Id = 4, Name = "Bodyweight" }
            );

            // 🏅 Specialties
            modelBuilder.Entity<Specialty>().HasData(
                new Specialty { Id = 1, Name = "Strength Training" },
                new Specialty { Id = 2, Name = "Cardio" },
                new Specialty { Id = 3, Name = "Nutrition" }
            );

            // 👨‍🏫 Coach Example
            modelBuilder.Entity<Coach>().HasData(
                new Coach
                {
                    Id = coachId,
                    Name = "John Smith",
                    Title = "Certified Personal Trainer",
                    About = "Experienced trainer specializing in strength and conditioning.",
                    ImagePath = "coach1.jpg",
                    IsActive = true,
                    UserId = Guid.Empty // مؤقتًا لو ما ربطتش المستخدمين بعد
                }
            );

            // 🧾 Package
            modelBuilder.Entity<Package>().HasData(
                new Package
                {
                    Id = 1,
                    Name = "Basic Package",
                    Price = 100,
                    Sessions = 8,
                    CoachId = coachId
                },
                new Package
                {
                    Id = 2,
                    Name = "Premium Package",
                    Price = 250,
                    Sessions = 20,
                    CoachId = coachId
                }
            );

            // 🧍 Client Example
            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = clientId,
                    Name = "Ahmed Ali",
                    Age = 28,
                    Height = 180,
                    StartWeight = 85,
                    Goal = "Lose 10kg",
                    Gender = "Male",
                    Image = "client1.jpg",
                    IsActive = true,
                    UserId = Guid.Empty,
                    CoachId = coachId,
                    PackageId = 1
                }
            );

            // 🧠 Exercises
            modelBuilder.Entity<Exercise>().HasData(
                new Exercise
                {
                    Id = 1,
                    Name = "Bicep Curl",
                    Description = "Perform curls using dumbbells to target biceps.",
                    EquipmentId = 1,
                    MuscleId = 1
                },
                new Exercise
                {
                    Id = 2,
                    Name = "Triceps Pushdown",
                    Description = "Cable exercise for triceps.",
                    EquipmentId = 3,
                    MuscleId = 2
                }
            );

            // 💬 Example Notification
            modelBuilder.Entity<Notification>().HasData(
                new Notification
                {
                    Id = 1,
                    ReciverId = Guid.Empty,
                    Content = "Welcome to FitVerse!",
                    CreatedAt = new DateTime(2025, 10, 11),
                    RefId = 0,
                    Type = 0,
                    IsRead = false
                }
                );
        }
       
    }
    
}
