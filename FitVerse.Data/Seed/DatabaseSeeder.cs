using FitVerse.Core.Models;
using FitVerse.Core.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider, bool includeSampleData = false)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Seed roles first
            await SeedRolesAsync(roleManager);

            // Seed default admin user
            await SeedDefaultAdminAsync(userManager);
            await SeedDefaultCoachAsync(userManager);
            await SeedDefaultClientAsync(userManager);

            // Optionally seed sample data for development/testing
            if (includeSampleData)
            {
                await SampleDataSeeder.SeedSampleDataAsync(serviceProvider, true);
            }
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            // Define the roles for the FitVerse application using constants
            string[] roles = RoleConstants.AllRoles;

            foreach (var role in roles)
            {
                // Check if the role already exists
                if (!await roleManager.RoleExistsAsync(role))
                {
                    // Create the role if it doesn't exist
                    var identityRole = new IdentityRole(role);
                    var result = await roleManager.CreateAsync(identityRole);

                    if (result.Succeeded)
                    {
                        Console.WriteLine($"Role '{role}' created successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Error creating role '{role}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
                else
                {
                    Console.WriteLine($"Role '{role}' already exists.");
                }
            }
        }

        private static async Task SeedDefaultAdminAsync(UserManager<ApplicationUser> userManager)
        {
            // Check if admin user already exists
            var adminUser = await userManager.FindByEmailAsync("admin@fitverse.com");
            
            if (adminUser == null)
            {
                // Create default admin user
                adminUser = new ApplicationUser
                {
                    UserName = "admin@fitverse.com",
                    Email = "admin@fitverse.com",
                    EmailConfirmed = true,
                    PhoneNumber = "1234567890",
                    PhoneNumberConfirmed = true,
                    Status = "Active"

                };

                // Create the user with default password
                var result = await userManager.CreateAsync(adminUser, "Admin@123");

                if (result.Succeeded)
                {
                    // Assign Admin role to the user
                    await userManager.AddToRoleAsync(adminUser, RoleConstants.Admin);
                    Console.WriteLine("Default admin user created successfully.");
                    Console.WriteLine("Email: admin@fitverse.com");
                    Console.WriteLine("Password: Admin@123");
                    Console.WriteLine("Please change the password after first login!");
                }
                else
                {
                    Console.WriteLine($"Error creating admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                Console.WriteLine("Admin user already exists.");
                
                // Ensure admin user has Admin role
                if (!await userManager.IsInRoleAsync(adminUser, RoleConstants.Admin))
                {
                    await userManager.AddToRoleAsync(adminUser, RoleConstants.Admin);
                    Console.WriteLine("Admin role assigned to existing admin user.");
                }
            }
        }
        private static async Task SeedDefaultCoachAsync(UserManager<ApplicationUser> userManager)
        {
            // Check if admin user already exists
            var adminUser = await userManager.FindByEmailAsync("coach@fitverse.com");

            if (adminUser == null)
            {
                // Create default admin user
                adminUser = new ApplicationUser
                {
                    UserName = "coach@fitverse.com",
                    Email = "coach@fitverse.com",
                    EmailConfirmed = true,
                    PhoneNumber = "1234567890",
                    PhoneNumberConfirmed = true,
                    Status = "Active"

                };

                // Create the user with default password
                var result = await userManager.CreateAsync(adminUser, "Coach@123");

                if (result.Succeeded)
                {
                    // Assign Admin role to the user
                    await userManager.AddToRoleAsync(adminUser, RoleConstants.Coach);
                    Console.WriteLine("Default coach user created successfully.");
                    Console.WriteLine("Email: coach@fitverse.com");
                    Console.WriteLine("Password: coach@123");
                    Console.WriteLine("Please change the password after first login!");
                }
                else
                {
                    Console.WriteLine($"Error creating coach user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                Console.WriteLine("Admin user already exists.");

                // Ensure admin user has Admin role
                if (!await userManager.IsInRoleAsync(adminUser, RoleConstants.Coach))
                {
                    await userManager.AddToRoleAsync(adminUser, RoleConstants.Coach);
                    Console.WriteLine("Admin role assigned to existing admin user.");
                }
            }
        }
        private static async Task SeedDefaultClientAsync(UserManager<ApplicationUser> userManager)
        {
            // Check if admin user already exists
            var adminUser = await userManager.FindByEmailAsync("client@fitverse.com");

            if (adminUser == null)
            {
                // Create default admin user
                adminUser = new ApplicationUser
                {
                    UserName = "client@fitverse.com",
                    Email = "client@fitverse.com",
                    EmailConfirmed = true,
                    PhoneNumber = "1234567890",
                    PhoneNumberConfirmed = true,
                    Status= "Active"

                };

                // Create the user with default password
                var result = await userManager.CreateAsync(adminUser, "Client@123");

                if (result.Succeeded)
                {
                    // Assign Client role to the user
                    await userManager.AddToRoleAsync(adminUser, RoleConstants.Client);
                    Console.WriteLine("Default client user created successfully.");
                    Console.WriteLine("Email: client@fitverse.com");
                    Console.WriteLine("Password: Client@123");
                    Console.WriteLine("Please change the password after first login!");
                }
                else
                {
                    Console.WriteLine($"Error creating client user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                Console.WriteLine("Admin user already exists.");

                // Ensure admin user has Admin role
                if (!await userManager.IsInRoleAsync(adminUser, RoleConstants.Client))
                {
                    await userManager.AddToRoleAsync(adminUser, RoleConstants.Client);
                    Console.WriteLine("Admin role assigned to existing admin user.");
                }
            }
        }
    }
}
