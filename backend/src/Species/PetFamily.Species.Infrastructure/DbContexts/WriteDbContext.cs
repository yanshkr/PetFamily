using Microsoft.EntityFrameworkCore;
using PetFamily.Species.Domain.Entities;

namespace PetFamily.Species.Infrastructure.DbContexts;
public class WriteDbContext(DbContextOptions<WriteDbContext> options) : DbContext(options)
{
    public DbSet<PetSpecie> PetSpecies => Set<PetSpecie>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);

        base.OnModelCreating(modelBuilder);
    }
}
