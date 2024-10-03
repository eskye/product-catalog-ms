using Catalog.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Infrastructure.Persistence.Contexts;

namespace Product.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration config)
    {
        services.AddPersistenceInfrastructure(config)
            .AddHttpService();
        return services;
    }

    private static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ProductDbContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("ProductDbConnection"),
                    b => b.MigrationsAssembly(typeof(ProductDbContext).Assembly.FullName))
                .EnableSensitiveDataLogging(false);

        });
        return services;
    }

}

