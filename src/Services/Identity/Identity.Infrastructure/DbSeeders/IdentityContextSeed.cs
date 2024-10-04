using Identity.Domain;
using Identity.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Npgsql;
using Polly;
using Polly.Retry;

namespace Identity.Infrastructure.DbSeeders
{
    public class IdentityContextSeed
    {
        /// <summary>
        /// Seed the users, roles, and account types table with some data
        /// </summary>
        /// <param name="context"></param>
        /// <param name="log"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public async Task SeedAsync(IdentityDbContext context, ILogger<IdentityContextSeed> log, UserManager<User> userManager, RoleManager<IdentityRole<long>> roleManager)
        {
            var policy = CreatePolicy(log, nameof(IdentityContextSeed));
            await policy.ExecuteAsync(async () =>
            {
                await using (context)
                {
                    //await DefaultRoles.SeedAsync(roleManager, log);
                    await DefaultUsers.SeedAsync(userManager, roleManager, context);
                }
            });
        }

        /// <summary>
        /// Handles retry if the process fails using <see cref="Polly"/>
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="prefix"></param>
        /// <param name="retries"></param>
        /// <returns></returns>
        private AsyncRetryPolicy CreatePolicy(ILogger<IdentityContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<PostgresException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }
}

