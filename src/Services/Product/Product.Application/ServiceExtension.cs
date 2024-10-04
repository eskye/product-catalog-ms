using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Product.Application;

public static class ServiceExtension
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); 
        return services;
    }
}

