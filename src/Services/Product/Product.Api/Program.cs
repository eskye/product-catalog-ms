using Catalog.Shared;
using Catalog.Shared.Middlewares;
using Product.Api;
using Product.Infrastructure;
using Product.Application;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Information()
                   .WriteTo.Console()
                   .CreateLogger();

// Add services to the container.
builder.Services.AddApplicationLayer()
    .AddCurrentUserService()
    .AddInfrastructureLayer(configuration)
    .AddApiVersioningExtension()
    .AddWebCoreServices(string.Empty);
 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization(); 
app.UseAuthentication(); 

app.MapControllers();

app.Run();

