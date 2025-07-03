using Microsoft.EntityFrameworkCore;
using PetFamily.Species.Application;
using PetFamily.Species.Contracts.Dtos.Breed;
using PetFamily.Species.Contracts.Dtos.Specie;

namespace PetFamily.Species.Infrastructure.DbContexts;
public class ReadDbContext(DbContextOptions<ReadDbContext> options) : DbContext(options), IReadDbContext
{
    public IQueryable<SpecieDto> Species => Set<SpecieDto>();
    public IQueryable<BreedDto> Breeds => Set<BreedDto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
        
        modelBuilder.HasDefaultSchema("species");

        base.OnModelCreating(modelBuilder);
    }
}