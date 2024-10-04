using Catalog.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Interfaces.Repositories;
using Product.Application.Interfaces.Services;
using Product.Infrastructure.Persistence.Contexts;
using Product.Infrastructure.Repositories;
using Product.Infrastructure.Services;
using static System.Formats.Asn1.AsnWriter;

namespace Product.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration config)
    {
        services.AddPersistenceInfrastructure(config)
            .AddHttpService()
            .AddCustomerServices();
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
        if (config.GetValue<bool>("AutoMigrate"))
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
            dbContext.Database.Migrate();
           
        }
        return services;
    }

    public static IServiceCollection AddCustomerServices(this IServiceCollection services)
    {
        return services
            .AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>))
            .AddTransient<IProductRepository, ProductRepository>() 
            .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
            .AddTransient<IProductService, ProductService>();
    }
}

