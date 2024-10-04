using System.Reflection;
using Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Identity.Infrastructure.Persistence.Contexts
{
    public class IdentityDbContext : IdentityDbContext<User, IdentityRole<long>, long>
    { 

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            var tableNameWithAspNet = builder.Model.GetEntityTypes().Where(e => e.GetTableName().StartsWith("AspNet")).ToList();
            tableNameWithAspNet.ForEach(x =>
            {
                x.SetTableName(x.GetTableName().Substring(6));
            });
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

