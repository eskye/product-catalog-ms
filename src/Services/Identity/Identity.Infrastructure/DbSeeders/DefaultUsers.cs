using System.Data;
using Identity.Application.Constants;
using Identity.Domain;
using Identity.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.DbSeeders
{
    public static class DefaultUsers
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole<long>> roleManager, IdentityDbContext dbContext)
        {
            //Eager run the seedings
            Task.Run(async () =>
            {
                //Check if Role Exists
                var adminRole = new IdentityRole<long>(Role.ADMINISTRATORS);
                var adminRoleInDb = await roleManager.FindByNameAsync(Role.ADMINISTRATORS);
                if (adminRoleInDb == null)
                {
                    await roleManager.CreateAsync(adminRole);
                }
                //Check if User Exists
                var adminUser = new User
                {
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@identity.co",
                    UserName = "admin@identity.co",
                    EmailConfirmed = true,
                    PhoneNumber = "09000000000",
                    PhoneNumberConfirmed = true,
                    IsActive = true
                };
                var adminUserInDb = await userManager.FindByEmailAsync(adminUser.Email);
                if (adminUserInDb == null)
                {
                    await userManager.CreateAsync(adminUser, Authorization.DEFAULT_PASSWORD);
                    await userManager.AddToRoleAsync(adminUser, Role.ADMINISTRATORS);
                }
            }).GetAwaiter().GetResult();


            //Seed Default SuperAdmin User
            Task.Run(async () => 
            {
                //Check if Role Exists
                var superAdminRole = new IdentityRole<long>(Role.SUPERADMIN);
                var superAdminRoleInDb = await roleManager.FindByNameAsync(Role.SUPERADMIN);
                if (superAdminRoleInDb == null)
                {
                    await roleManager.CreateAsync(superAdminRole);
                }
                //Check if User Exists
                var superAdminUser = new User
                {
                    FirstName = "SuperAdmin",
                    LastName = "User",
                    Email = "superadmin@identity.co",
                    UserName = "superadmin@identity.co",
                    EmailConfirmed = true,
                    PhoneNumber = "070352222232",
                    PhoneNumberConfirmed = true,
                    IsActive = true
                };
                var superadminUserInDb = await userManager.FindByEmailAsync(superAdminUser.Email);
                if (superadminUserInDb == null)
                {
                    await userManager.CreateAsync(superAdminUser, Authorization.DEFAULT_PASSWORD);
                    await userManager.AddToRoleAsync(superAdminUser, Role.SUPERADMIN);
                }
            }).GetAwaiter().GetResult();
            dbContext.SaveChanges();
            await Task.CompletedTask;
        }

    }

}

