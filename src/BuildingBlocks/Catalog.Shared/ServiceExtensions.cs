using System;
using Catalog.Shared.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Shared
{
	public static class ServiceExtensions
    { 
        public static IServiceCollection AddCurrentUserService(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>(); 
            return services;
        }

        public static IServiceCollection AddHttpService(this IServiceCollection services)
        { 
            services.AddHttpClient();
            services.AddScoped<IHttpClientService, HttpClientService>();
            return services;
        }
    }
}

