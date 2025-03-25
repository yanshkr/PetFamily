using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PetFamily.Application.Database;
using PetFamily.Infrastructure.DbContexts;
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
        services.RemoveAll(typeof(ReadDbContext));
        services.RemoveAll(typeof(WriteDbContext));

        services.AddDbContext<IReadDbContext, ReadDbContext>(options =>
        {
            options.UseNpgsql(postgreSqlContainer.GetConnectionString());
            options.UseSnakeCaseNamingConvention();
        });

        services.AddDbContext<WriteDbContext>(options =>
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
        var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
    }

    public new async Task DisposeAsync()
    {
        await postgreSqlContainer.StopAsync();
        await postgreSqlContainer.DisposeAsync();
    }
}
