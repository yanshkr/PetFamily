using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Species;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Infrastructure.DbContexts;
public class WriteDbContext(DbContextOptions<WriteDbContext> options) : DbContext(options)
{
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    public DbSet<PetSpecie> PetSpecies => Set<PetSpecie>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);

        base.OnModelCreating(modelBuilder);
    }
}
