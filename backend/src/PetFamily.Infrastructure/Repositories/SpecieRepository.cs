using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Features.Species;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;
using PetFamily.Domain.Species.Ids;

namespace PetFamily.Infrastructure.Repositories;
public class SpecieRepository : ISpeciesRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SpecieRepository(ApplicationDbContext dbContext)
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
