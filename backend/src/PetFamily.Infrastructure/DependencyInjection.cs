using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.AspNetCore;
using PetFamily.Application.Database;
using PetFamily.Application.Features.Species;
using PetFamily.Application.Features.Volunteers;
using PetFamily.Application.Providers;
using PetFamily.Infrastructure.Constants;
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
            .InjectMinio(configuration);
    }
    public static IServiceCollection InjectDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Database"));
            options.UseSnakeCaseNamingConvention();
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
}