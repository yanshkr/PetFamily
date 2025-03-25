using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;

namespace PetFamily.Infrastructure.DbContexts;
public class ReadDbContext(DbContextOptions<ReadDbContext> options) : DbContext(options), IReadDbContext
{
    public IQueryable<VolunteerDto> Volunteers => Set<VolunteerDto>();
    public IQueryable<PetDto> Pets => Set<PetDto>();
    public IQueryable<SpecieDto> Species => Set<SpecieDto>();
    public IQueryable<BreedDto> Breeds => Set<BreedDto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);

        base.OnModelCreating(modelBuilder);
    }
}