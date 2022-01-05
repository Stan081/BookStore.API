using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Repository.Helpers
{
    public class Seeders
    {
        public static async Task SeedRoles(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roles = new List<IdentityRole>
            {
                new IdentityRole{Name = "Manager"},
                new IdentityRole{Name = "Admin"},
                new IdentityRole{Name = "Customer"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            var admin = new User
            {
                FName = "Super",
                LName = "Admin",
                Email = "manager@gmail.com",
                UserName = "manager@gmail.com"
            };

            var added = await userManager.CreateAsync(admin, "_Manager007");
            Console.Out.Write(added.ToString());
            await userManager.AddToRolesAsync(admin, new[] {"Manager","Admin"});
        }
    }
}
