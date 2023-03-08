using System.Text;
using Application.Common.Interfaces;
using Domain;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistence;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
        IConfiguration config)
    {
        services.AddControllers(options =>
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        });
        
        services.AddIdentityCore<AuthUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<BlabberContext>()
            .AddSignInManager<SignInManager<AuthUser>>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(config["TokenKey"]!)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        services.AddDbContext<BlabberContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"), 
                b => b.MigrationsAssembly("API"));
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        });
        
        services.AddScoped<ITokenService, TokenService>();
        
        return services;
    }
}