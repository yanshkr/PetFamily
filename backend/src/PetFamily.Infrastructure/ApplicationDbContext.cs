using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Species;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Infrastructure;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    public DbSet<PetSpecie> PetSpecies => Set<PetSpecie>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
