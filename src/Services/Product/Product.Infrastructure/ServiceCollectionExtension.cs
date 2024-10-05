using System.Text;
using Catalog.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Product.Application.Interfaces.Repositories;
using Product.Application.Interfaces.Services;
using Product.Infrastructure.Persistence.Contexts;
using Product.Infrastructure.Repositories;
using Product.Infrastructure.Services;

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

    public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(sharedOptions =>
        {
            sharedOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            sharedOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, cfg =>
        {
            cfg.RequireHttpsMetadata = false; 
            cfg.SaveToken = true;

            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["JwtSettings:Issuer"],
                ValidAudience = config["JwtSettings:Audience"],
                RequireExpirationTime = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["JwtSettings:SecretKey"]))
            };
            cfg.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    Console.WriteLine("OnTokenValidated: " + context.Result);
                    return Task.CompletedTask;
                }
            };
        });

        return services;

    }
}

