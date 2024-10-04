using Identity.Domain;
using Identity.Infrastructure.DbSeeders;
using Identity.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Identity.Api.Extensions
{
    public static class HostBuilderExtension
	{
        internal static async Task SeedDatabase(this WebApplication builder)
        {
            using var scope = builder.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();
            try
            {
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole<long>>>();
                var contextLogger = services.GetRequiredService<ILogger<IdentityContextSeed>>();
                var identityDbContext = services.GetRequiredService<IdentityDbContext>();
                await new IdentityContextSeed().SeedAsync(identityDbContext, contextLogger, userManager, roleManager);
                logger.LogInformation("Finished Seeding Default Data");
                logger.LogInformation("Application Starting");
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "An error occurred seeding the DB");
            }
            finally
            {
                Log.CloseAndFlush();
            }


        }
    }
}

