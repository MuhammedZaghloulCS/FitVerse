using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Seed
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Define the roles for the FitVerse application
            string[] roles = { "Admin", "Coach", "Client" };

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
    }
}
