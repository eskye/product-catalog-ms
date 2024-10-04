using System.Reflection;
using Catalog.Shared.Application;
using Catalog.Shared.Domain.Common;
using Microsoft.EntityFrameworkCore;
using ProductEntity = Product.Domain.Entities.Product;
namespace Product.Infrastructure.Persistence.Contexts
{
	public class ProductDbContext : DbContext
	{
        private readonly ICurrentUserService _currentUser;

        public ProductDbContext(DbContextOptions<ProductDbContext> options, ICurrentUserService currentUser): base(options)
		{
            _currentUser = currentUser;
        }

        public DbSet<ProductEntity> Products { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.DateCreated = DateTime.UtcNow;
                        entry.Entity.CreatedBy = _currentUser.UserId != Guid.Empty ? _currentUser.UserId : entry.Entity.CreatedBy;
                        entry.Entity.Active = true;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = _currentUser.UserId != Guid.Empty ? _currentUser.UserId : entry.Entity.CreatedBy;
                        break;
                }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        }
    }
}

