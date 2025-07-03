using Microsoft.EntityFrameworkCore;
using PetFamily.Volunteers.Domain.Entities;

namespace PetFamily.Volunteers.Infrastructure.DbContexts;
public class WriteDbContext(DbContextOptions<WriteDbContext> options) : DbContext(options)
{
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);
        
        modelBuilder.HasDefaultSchema("volunteers");

        base.OnModelCreating(modelBuilder);
    }
}
