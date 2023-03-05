using Application.Auth.Commands;
using Application.Common.AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Authentication Token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JsonWebToken",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
        
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:3000");
            });
        });

        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        
        services.AddMediatR(typeof(LoginCommand).Assembly);
        services.AddFluentValidation(config =>
        {
            config.ImplicitlyValidateChildProperties = true;
            config.ImplicitlyValidateRootCollectionElements = true;
            config.RegisterValidatorsFromAssemblyContaining<LoginCommand>();
        });
        
        return services;
    }
}