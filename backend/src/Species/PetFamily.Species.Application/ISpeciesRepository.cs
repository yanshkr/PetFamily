using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.Species.Domain.Entities;
using PetFamily.Species.Domain.Ids;

namespace PetFamily.Species.Application;
public interface ISpeciesRepository
{
    Task<PetSpecieId> AddAsync(PetSpecie petSpecie, CancellationToken cancellationToken = default);
    Task<Result<PetSpecie, Error>> GetByIdAsync(PetSpecieId id, CancellationToken cancellationToken = default);
    Result<PetSpecieId> Delete(PetSpecie petSpecie);
}