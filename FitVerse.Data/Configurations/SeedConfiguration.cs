using FitVerse.Core.Models;
using FitVerse.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace FitVerse.Data.DataSeeding
{
    public static class DataSeeder
    {
        public static void SeedConfiguration(this ModelBuilder modelBuilder)
        {
            // Seed Anatomies
            modelBuilder.Entity<Anatomy>().HasData(
                new Anatomy { Id = 1, Name = "Chest", Image = "/images/anatomy/chest.jpg" },
                new Anatomy { Id = 2, Name = "Back", Image = "/images/anatomy/back.jpg" },
                new Anatomy { Id = 3, Name = "Arms", Image = "/images/anatomy/arms.jpg" },
                new Anatomy { Id = 4, Name = "Shoulders", Image = "/images/anatomy/shoulders.jpg" },
                new Anatomy { Id = 5, Name = "Legs", Image = "/images/anatomy/legs.jpg" },
                new Anatomy { Id = 6, Name = "Core", Image = "/images/anatomy/core.jpg" }
            );

            // Seed Equipments
            modelBuilder.Entity<Equipment>().HasData(
                new Equipment { Id = 1, Name = "Barbell", Image = "/images/equipment/barbell.jpg" },
                new Equipment { Id = 2, Name = "Dumbbell", Image = "/images/equipment/dumbbell.jpg" },
                new Equipment { Id = 3, Name = "Kettlebell", Image = "/images/equipment/kettlebell.jpg" },
                new Equipment { Id = 4, Name = "Resistance Bands", Image = "/images/equipment/bands.jpg" },
                new Equipment { Id = 5, Name = "Bodyweight", Image = "/images/equipment/bodyweight.jpg" },
                new Equipment { Id = 6, Name = "Cable Machine", Image = "/images/equipment/cable.jpg" },
                new Equipment { Id = 7, Name = "Machine", Image = "/images/equipment/machine.jpg" }
            );

            // Seed Muscles
            modelBuilder.Entity<Muscle>().HasData(
                // Chest
                new Muscle { Id = 1, Name = "Pectoralis Major", Description = "Main chest muscle", AnatomyId = 1, ImagePath = "/images/muscles/pectoralis-major.jpg" },
                new Muscle { Id = 2, Name = "Pectoralis Minor", Description = "Underneath pectoralis major", AnatomyId = 1, ImagePath = "/images/muscles/pectoralis-minor.jpg" },
                // Back
                new Muscle { Id = 3, Name = "Latissimus Dorsi", Description = "Wide back muscle", AnatomyId = 2, ImagePath = "/images/muscles/lats.jpg" },
                new Muscle { Id = 4, Name = "Trapezius", Description = "Upper back muscle", AnatomyId = 2, ImagePath = "/images/muscles/traps.jpg" },
                // Arms
                new Muscle { Id = 5, Name = "Biceps Brachii", Description = "Front upper arm", AnatomyId = 3, ImagePath = "/images/muscles/biceps.jpg" },
                new Muscle { Id = 6, Name = "Triceps Brachii", Description = "Back upper arm", AnatomyId = 3, ImagePath = "/images/muscles/triceps.jpg" },
                // Shoulders
                new Muscle { Id = 7, Name = "Deltoid", Description = "Shoulder muscle", AnatomyId = 4, ImagePath = "/images/muscles/deltoid.jpg" },
                // Legs
                new Muscle { Id = 8, Name = "Quadriceps", Description = "Front thigh", AnatomyId = 5, ImagePath = "/images/muscles/quads.jpg" },
                new Muscle { Id = 9, Name = "Hamstrings", Description = "Back thigh", AnatomyId = 5, ImagePath = "/images/muscles/hamstrings.jpg" },
                new Muscle { Id = 10, Name = "Gluteus Maximus", Description = "Buttocks", AnatomyId = 5, ImagePath = "/images/muscles/glutes.jpg" },
                // Core
                new Muscle { Id = 11, Name = "Rectus Abdominis", Description = "Six-pack muscle", AnatomyId = 6, ImagePath = "/images/muscles/abs.jpg" },
                new Muscle { Id = 12, Name = "Obliques", Description = "Side abdominal muscles", AnatomyId = 6, ImagePath = "/images/muscles/obliques.jpg" }
            );

            // Seed Exercises
            modelBuilder.Entity<Exercise>().HasData(
                // Chest Exercises
                new Exercise { Id = 1, Name = "Barbell Bench Press", VideoLink = "https://www.youtube.com/watch?v=4Y2ZdHCOXok", Description = "Flat bench press with barbell", MuscleId = 1, EquipmentId = 1 },
                new Exercise { Id = 2, Name = "Dumbbell Flyes", VideoLink = "https://www.youtube.com/watch?v=eGo4IYlbE5g", Description = "Chest fly with dumbbells", MuscleId = 1, EquipmentId = 2 },
                // Back Exercises
                new Exercise { Id = 3, Name = "Pull-ups", VideoLink = "https://www.youtube.com/watch?v=eGo4IYlbE5g", Description = "Bodyweight pull-up exercise", MuscleId = 3, EquipmentId = 5 },
                new Exercise { Id = 4, Name = "Bent Over Rows", VideoLink = "https://www.youtube.com/watch?v=Z_3xHwuO8Tk", Description = "Barbell bent over rows", MuscleId = 3, EquipmentId = 1 },
                // Shoulder Exercises
                new Exercise { Id = 5, Name = "Military Press", VideoLink = "https://www.youtube.com/watch?v=2yjwXTZQDDI", Description = "Overhead barbell press", MuscleId = 7, EquipmentId = 1 },
                new Exercise { Id = 6, Name = "Lateral Raises", VideoLink = "https://www.youtube.com/watch?v=3VcKaXpzqRo", Description = "Dumbbell lateral raises", MuscleId = 7, EquipmentId = 2 },
                // Arm Exercises
                new Exercise { Id = 7, Name = "Barbell Curl", VideoLink = "https://www.youtube.com/watch?v=kwG2ipFRgfo", Description = "Standing barbell bicep curl", MuscleId = 5, EquipmentId = 1 },
                new Exercise { Id = 8, Name = "Tricep Pushdown", VideoLink = "https://www.youtube.com/watch?v=2-LAMcpzODU", Description = "Cable tricep pushdown", MuscleId = 6, EquipmentId = 6 },
                // Leg Exercises
                new Exercise { Id = 9, Name = "Barbell Squat", VideoLink = "https://www.youtube.com/watch?v=SW_C1A-rejs", Description = "Back squat with barbell", MuscleId = 8, EquipmentId = 1 },
                new Exercise { Id = 10, Name = "Romanian Deadlift", VideoLink = "https://www.youtube.com/watch?v=2SHsk9AzdjA", Description = "Barbell romanian deadlift", MuscleId = 9, EquipmentId = 1 },
                // Core Exercises
                new Exercise { Id = 11, Name = "Hanging Leg Raise", VideoLink = "https://www.youtube.com/watch?v=PrJEWdHptJk", Description = "Hanging leg raises for abs", MuscleId = 11, EquipmentId = 5 },
                new Exercise { Id = 12, Name = "Russian Twists", VideoLink = "https://www.youtube.com/watch?v=wkD8rjkodUI", Description = "Seated russian twists", MuscleId = 12, EquipmentId = 5 }
            );

            // Seed Packages
            modelBuilder.Entity<Package>().HasData(
                new Package { Id = 1, Name = "Basic", Price = 49.99, Sessions = 8, Description = "Basic fitness package with 8 sessions", IsActive = true },
                new Package { Id = 2, Name = "Standard", Price = 89.99, Sessions = 16, Description = "Standard package with 16 sessions", IsActive = true },
                new Package { Id = 3, Name = "Premium", Price = 129.99, Sessions = 24, Description = "Premium package with 24 sessions", IsActive = true },
                new Package { Id = 4, Name = "Elite", Price = 199.99, Sessions = 36, Description = "Elite package with 36 sessions", IsActive = true }
            );
        }

        public static async Task SeedUsersAndRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Seed Roles
            string[] roleNames = { "Admin", "Coach", "Client" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed Admin User
            var adminEmail = "admin@fitverse.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true,
                    joinedDate = DateTime.Now,
                    Status = "Active"
                };

                var createAdmin = await userManager.CreateAsync(admin, "Admin@123");
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            // Seed Sample Coach
            var coachEmail = "coach@fitverse.com";
            var coachUser = await userManager.FindByEmailAsync(coachEmail);
            if (coachUser == null)
            {
                var coach = new ApplicationUser
                {
                    UserName = "coach",
                    Email = coachEmail,
                    EmailConfirmed = true,
                    joinedDate = DateTime.Now,
                    Status = "Active"
                };

                var createCoach = await userManager.CreateAsync(coach, "Coach@123");
                if (createCoach.Succeeded)
                {
                    await userManager.AddToRoleAsync(coach, "Coach");
                }
            }

            // Seed Sample Client
            var clientEmail = "client@fitverse.com";
            var clientUser = await userManager.FindByEmailAsync(clientEmail);
            if (clientUser == null)
            {
                var client = new ApplicationUser
                {
                    UserName = "client",
                    Email = clientEmail,
                    EmailConfirmed = true,
                    joinedDate = DateTime.Now,
                    Status = "Active"
                };

                var createClient = await userManager.CreateAsync(client, "Client@123");
                if (createClient.Succeeded)
                {
                    await userManager.AddToRoleAsync(client, "Client");
                }
            }
        }
    }
}