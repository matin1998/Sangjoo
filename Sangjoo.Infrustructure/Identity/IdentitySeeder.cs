using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sangjoo.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Sangjoo.infrastructure.Identity;
public static class IdentitySeeder
{
    public static async Task SeedRolesAsync(
        RoleManager<IdentityRole<long>> roleManager)
    {
        string[] roles =
        {
            "Admin",
            "Seller"
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(
                    new IdentityRole<long>
                    {
                        Name = role
                    });
            }
        }
    }
    public static async Task SeedAdminAsync(
        UserManager<ApplicationUser> userManager)
    {
        const string email = "admin@sangjoo.ir";
        const string password = "Admin@123";

        var admin =
            await userManager.FindByEmailAsync(email);

        if (admin == null)
        {
            admin = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = "مدیر سیستم",
                IsActive = true,
                EmailConfirmed = true
            };

            var result =
                await userManager.CreateAsync(
                    admin,
                    password);

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(" | ",
                        result.Errors.Select(x => x.Description)));
            }
        }

        if (!await userManager.IsInRoleAsync(admin, "Admin"))
        {
            var result =
                await userManager.AddToRoleAsync(
                    admin,
                    "Admin");

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(" | ",
                        result.Errors.Select(x => x.Description)));
            }
        }
    }
}