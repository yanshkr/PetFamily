using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.AspNetCore;
using PetFamily.Application.Background;
using PetFamily.Application.Database;
using PetFamily.Application.Features.Commands.Species;
using PetFamily.Application.Features.Commands.Volunteers;
using PetFamily.Application.Providers;
using PetFamily.Infrastructure.BackgroudServices;
using PetFamily.Infrastructure.Constants;
using PetFamily.Infrastructure.DbContexts;
using PetFamily.Infrastructure.MessageQueues;
using PetFamily.Infrastructure.Providers;
using PetFamily.Infrastructure.Repositories;

namespace PetFamily.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .InjectDatabase(configuration)
            .InjectMinio(configuration)
            .InjectFilesCleaner();
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

        services.AddScoped<IVolunteersRepository, VolunteerRepository>();
        services.AddScoped<ISpeciesRepository, SpecieRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
    public static IServiceCollection InjectMinio(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MinioOptions>(configuration.GetSection(MinioConstants.OPTIONS_NAME));
        services.AddMinio(configure =>
        {
            configure = configuration.GetSection(MinioConstants.OPTIONS_NAME).Get<MinioOptions>()
                ?? throw new ArgumentNullException(nameof(MinioOptions));
        });

        services.AddSingleton<IFilesProvider, MinioProvider>();

        return services;
    }
    public static IServiceCollection InjectFilesCleaner(this IServiceCollection services)
    {
        services.AddHostedService<RedudantFileCleanerService>();
        services.AddSingleton<IFileCleanerQueue, FileCleanerQueue>();

        return services;
    }
}