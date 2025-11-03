using FitVerse.Core.Models;
using FitVerse.Core.Helpers;
using FitVerse.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Seed
{
    public static class SampleDataSeeder
    {
        public static async Task SeedSampleDataAsync(IServiceProvider serviceProvider, bool seedSampleUsers = false)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (seedSampleUsers)
            {
                await SeedSampleUsersAsync(userManager);
            }
        }

        private static async Task SeedSampleUsersAsync(UserManager<ApplicationUser> userManager)
        {
            // Sample Coach
            var sampleCoach = await userManager.FindByEmailAsync("coach@fitverse.com");
            if (sampleCoach == null)
            {
                sampleCoach = new ApplicationUser
                {
                    UserName = "coach@fitverse.com",
                    Email = "coach@fitverse.com",
                    EmailConfirmed = true,
                    PhoneNumber = "1234567891",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(sampleCoach, "Coach@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(sampleCoach, RoleConstants.Coach);
                    Console.WriteLine("Sample coach user created: coach@fitverse.com / Coach@123");
                }
            }

            // Sample Client
            var sampleClient = await userManager.FindByEmailAsync("client@fitverse.com");
            if (sampleClient == null)
            {
                sampleClient = new ApplicationUser
                {
                    UserName = "client@fitverse.com",
                    Email = "client@fitverse.com",
                    EmailConfirmed = true,
                    PhoneNumber = "1234567892",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(sampleClient, "Client@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(sampleClient, RoleConstants.Client);
                    Console.WriteLine("Sample client user created: client@fitverse.com / Client@123");
                }
            }

            // Additional sample coach
            var sampleCoach2 = await userManager.FindByEmailAsync("alex.coach@fitverse.com");
            if (sampleCoach2 == null)
            {
                sampleCoach2 = new ApplicationUser
                {
                    UserName = "alex.coach@fitverse.com",
                    Email = "alex.coach@fitverse.com",
                    EmailConfirmed = true,
                    PhoneNumber = "1234567893",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(sampleCoach2, "Coach@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(sampleCoach2, RoleConstants.Coach);
                    Console.WriteLine("Sample coach user created: alex.coach@fitverse.com / Coach@123");
                }
            }

            // Additional sample client
            var sampleClient2 = await userManager.FindByEmailAsync("mike.client@fitverse.com");
            if (sampleClient2 == null)
            {
                sampleClient2 = new ApplicationUser
                {
                    UserName = "mike.client@fitverse.com",
                    Email = "mike.client@fitverse.com",
                    EmailConfirmed = true,
                    PhoneNumber = "1234567894",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(sampleClient2, "Client@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(sampleClient2, RoleConstants.Client);
                    Console.WriteLine("Sample client user created: mike.client@fitverse.com / Client@123");
                }
            }
        }
    }
}
