using GymManagement.Api.Services;
using GymManagement.Application.Common.Interfaces;
using Microsoft.OpenApi.Models;
using System.Runtime.CompilerServices;

namespace GymManagement.Api;

public static class DependecyInjection
{
    public static IServiceCollection AddPresentaion(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHttpContextAccessor();
        services.AddProblemDetails();
        services.AddTokenSupportForSwaggerUI();

        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

        return services;
    }

    private static IServiceCollection AddTokenSupportForSwaggerUI(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            // Add JWT Authentication in Swagger
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter your JWT token in the format: Bearer {token}",
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
            new string[] {} // No specific scopes are required
        }
    });
        });

        return services;
    }
}
