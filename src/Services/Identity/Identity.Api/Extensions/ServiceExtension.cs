using Catalog.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Extensions
{
    internal static class ServiceExtensions
    {
        internal static IServiceCollection AddApiVersioningExtension(this IServiceCollection services)
        {
            return services.AddApiVersioning(config =>
            {
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;

            }).AddVersionedApiExplorer(x =>
            {
                x.GroupNameFormat = "'v'VVV";
                x.SubstituteApiVersionInUrl = true;
            });
        }

        internal static void AddWebCoreServices(this IServiceCollection services, string allowedSpecificOrigins)
        {
            services.AddSingleton<ModelStateValidationFilter>();
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            services.Configure<MvcOptions>(options => options.Filters.AddService<ModelStateValidationFilter>());

            services.AddControllers();
            services.AddRouting(options => options.LowercaseUrls = true)
            .AddCors(options =>
            {
                options.AddPolicy(allowedSpecificOrigins, builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .AllowAnyHeader();
                });
            });
        }
    }
}

