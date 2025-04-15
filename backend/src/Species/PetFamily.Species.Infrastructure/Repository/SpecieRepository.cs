using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.Species.Application;
using PetFamily.Species.Domain.Entities;
using PetFamily.Species.Domain.Ids;
using PetFamily.Species.Infrastructure.DbContexts;

namespace PetFamily.Species.Infrastructure.Repository;
public class SpecieRepository : ISpeciesRepository
{
    private readonly WriteDbContext _dbContext;

    public SpecieRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PetSpecieId> AddAsync(PetSpecie petSpecie, CancellationToken cancellationToken = default)
    {
        await _dbContext.PetSpecies.AddAsync(petSpecie, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return petSpecie.Id;
    }
    public async Task<Result<PetSpecie, Error>> GetByIdAsync(PetSpecieId id, CancellationToken cancellationToken = default)
    {
        var petSpecie = await _dbContext.PetSpecies
            .Include(p => p.Breeds)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (petSpecie is null)
            return Errors.General.NotFound(id);

        return petSpecie;
    }
    public Result<PetSpecieId> Delete(PetSpecie petSpecie)
    {
        _dbContext.PetSpecies.Remove(petSpecie);
        return petSpecie.Id;
    }
}
