using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOcelot();
builder.Services.AddSwaggerForOcelot(config);

builder.Services.AddAuthentication(sharedOptions =>
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
            Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
            return Task.CompletedTask;
        }
    };
});

builder.Configuration.AddJsonFile("ocelot.json");

var app = builder.Build();

app.UseHttpsRedirection();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.UseSwaggerForOcelotUI(option =>
{
    option.PathToSwaggerGenerator = "/swagger/docs";
});
app.MapControllers();

await app.UseOcelot();
await app.RunAsync();
