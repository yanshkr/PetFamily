using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Database;
using PetFamily.SharedKernel.Structs;
using PetFamily.Species.Application;
using PetFamily.Species.Infrastructure.DbContexts;
using PetFamily.Species.Infrastructure.Repository;

namespace PetFamily.Species.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddSpeciesInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .InjectDatabase(configuration);
    }
    public static IServiceCollection InjectDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IReadDbContext, ReadDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Database"));
            options.UseSnakeCaseNamingConvention();
            options.EnableSensitiveDataLogging();
            options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        });

        services.AddDbContext<WriteDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Database"));
            options.UseSnakeCaseNamingConvention();
            options.EnableSensitiveDataLogging();
            options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        });

        services.AddScoped<ISpeciesRepository, SpecieRepository>();
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(UnitOfWorkSelector.Species);

        return services;
    }
}