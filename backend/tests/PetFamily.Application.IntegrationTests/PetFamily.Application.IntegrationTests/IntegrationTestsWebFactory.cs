using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace PetFamily.Application.IntegrationTests;
public class IntegrationTestsWebFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer postgreSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres")
        .WithDatabase("pet_family")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(ConfigureDefaultServices);
    }
    protected virtual void ConfigureDefaultServices(IServiceCollection services)
    {
        ConfigureSpecies(services);
        ConfigureVolunteers(services);
    }

    private void ConfigureSpecies(IServiceCollection services)
    {
        services.RemoveAll(typeof(SpeciesReadDbContext));
        services.RemoveAll(typeof(SpeciesWriteDbContext));

        services.AddDbContext<SpeciesReadDbContextInterface, SpeciesReadDbContext>(options =>
        {
            options.UseNpgsql(postgreSqlContainer.GetConnectionString());
            options.UseSnakeCaseNamingConvention();
        });

        services.AddDbContext<SpeciesWriteDbContext>(options =>
        {
            options.UseNpgsql(postgreSqlContainer.GetConnectionString());
            options.UseSnakeCaseNamingConvention();
        });
    }
    private void ConfigureVolunteers(IServiceCollection services)
    {
        services.RemoveAll(typeof(VolunteersReadDbContext));
        services.RemoveAll(typeof(VolunteersWriteDbContext));

        services.AddDbContext<VolunteersReadDbContextInterface, VolunteersReadDbContext>(options =>
        {
            options.UseNpgsql(postgreSqlContainer.GetConnectionString());
            options.UseSnakeCaseNamingConvention();
        });

        services.AddDbContext<VolunteersWriteDbContext>(options =>
        {
            options.UseNpgsql(postgreSqlContainer.GetConnectionString());
            options.UseSnakeCaseNamingConvention();
        });
    }

    public async Task InitializeAsync()
    {
        await postgreSqlContainer.StartAsync();

        await ResetDatabaseAsync();
    }

    public async Task ResetDatabaseAsync()
    {
        using var scope = Services.CreateScope();
        var speciesDbContext = scope.ServiceProvider.GetRequiredService<SpeciesWriteDbContext>();
        var volunteersDbContext = scope.ServiceProvider.GetRequiredService<VolunteersWriteDbContext>();

        await speciesDbContext.Database.EnsureDeletedAsync();
        await volunteersDbContext.Database.EnsureDeletedAsync();

        await speciesDbContext.Database.MigrateAsync();
        await volunteersDbContext.Database.MigrateAsync();
    }
    public new async Task DisposeAsync()
    {
        await postgreSqlContainer.StopAsync();
        await postgreSqlContainer.DisposeAsync();
    }
}
