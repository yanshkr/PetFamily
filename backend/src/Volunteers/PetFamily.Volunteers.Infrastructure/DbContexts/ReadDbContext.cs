using Microsoft.EntityFrameworkCore;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Contracts.Dtos.Pet;
using PetFamily.Volunteers.Contracts.Dtos.Volunteer;

namespace PetFamily.Volunteers.Infrastructure.DbContexts;
public class ReadDbContext(DbContextOptions<ReadDbContext> options) : DbContext(options), IReadDbContext
{
    public IQueryable<VolunteerDto> Volunteers => Set<VolunteerDto>();
    public IQueryable<PetDto> Pets => Set<PetDto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);

        base.OnModelCreating(modelBuilder);
    }
}