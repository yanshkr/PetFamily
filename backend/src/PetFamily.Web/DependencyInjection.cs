using Microsoft.OpenApi.Models;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Infrastructure;
using PetFamily.Species.Application;
using PetFamily.Species.Infrastructure;
using PetFamily.Species.Presentation;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Infrastructure;
using PetFamily.Volunteers.Presentation;
using Serilog;
using Serilog.Events;

namespace PetFamily.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddLogging(
        this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Debug()
            .WriteTo.Seq(configuration.GetConnectionString("Seq")!)
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
            .CreateLogger();

        services.AddSerilog();

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "My API",
                Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    []
                }
            });
        });
        
        return services;
    }

    public static IServiceCollection AddAccountsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAccountsApplication();
        services.AddAccountsInfrastructure(configuration);
        
        return services;
    }
    public static IServiceCollection AddVolunteersModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddVolunteersPresentation();
        services.AddVolunteersInfrastructure(configuration);
        services.AddVolunteersApplication();

        return services;
    }
    public static IServiceCollection AddSpeciesModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSpeciesPresentation();
        services.AddSpeciesInfrastructure(configuration);
        services.AddSpeciesApplication();

        return services;
    }
}
