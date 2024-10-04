using Identity.Application.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Identity.Infrastructure.DbSeeders
{
    public static class DefaultRoles
    {
        /// <summary>
        /// Seed the identity role table with data.
        /// </summary>
        /// <param name="roleManager"></param>
        /// <param name="log"></param>
        public static async Task SeedAsync(RoleManager<IdentityRole<long>> roleManager, ILogger<IdentityContextSeed> log)
        {
            //Seed Roles
            var roleList = new List<IdentityRole<long>>
            {
                new IdentityRole<long>(Role.SUPERADMIN),
                new IdentityRole<long>(Role.ADMINISTRATORS),
                new IdentityRole<long>(Role.BASIC),
            };

            var roles = await roleManager.Roles.ToListAsync();
            if (!roles.Any())
            {
                log.LogDebug($"Adding {roleList} to the database", roleList);
                foreach (var role in roleList)
                {
                    await roleManager.CreateAsync(role);
                }
            }
            else
            {
                log.LogDebug("updating {roleList} in the database", roleList);
                foreach (var role in roleList)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name))
                        await roleManager.CreateAsync(role);
                }
            }
        }
    }

}

