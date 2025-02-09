using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;
using PetFamily.Domain.Species.Ids;

namespace PetFamily.Application.Features.Species;
public interface ISpeciesRepository
{
    Task<PetSpecieId> AddAsync(PetSpecie petSpecie, CancellationToken cancellationToken = default);
    Task<Result<PetSpecie, Error>> GetByIdAsync(PetSpecieId id, CancellationToken cancellationToken = default);
    Result<PetSpecieId> Delete(PetSpecie petSpecie);
}