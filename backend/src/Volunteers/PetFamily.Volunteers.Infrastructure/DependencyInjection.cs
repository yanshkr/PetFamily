using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Minio.AspNetCore;
using PetFamily.Core.Database;
using PetFamily.SharedKernel.Structs;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Application.Background;
using PetFamily.Volunteers.Application.Providers;
using PetFamily.Volunteers.Infrastructure.BackgroudServices;
using PetFamily.Volunteers.Infrastructure.Constants;
using PetFamily.Volunteers.Infrastructure.DbContexts;
using PetFamily.Volunteers.Infrastructure.MessageQueues;
using PetFamily.Volunteers.Infrastructure.Providers;
using PetFamily.Volunteers.Infrastructure.Repository;

namespace PetFamily.Volunteers.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddVolunteersInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .InjectDatabase(configuration)
            .InjectMinio(configuration)
            .InjectFilesCleaner();
    }

    private static IServiceCollection InjectDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IReadDbContext, ReadDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Database"));
            options.EnableSensitiveDataLogging();
            options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        });

        services.AddDbContext<WriteDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Database"));
            options.EnableSensitiveDataLogging();
            options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        });

        services.AddScoped<IVolunteersRepository, VolunteerRepository>();
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(UnitOfWorkSelector.Volunteers);

        return services;
    }

    private static IServiceCollection InjectMinio(
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

    private static IServiceCollection InjectFilesCleaner(this IServiceCollection services)
    {
        services.AddHostedService<RedudantFileCleanerService>();
        services.AddSingleton<IFileCleanerQueue, FileCleanerQueue>();

        return services;
    }
}