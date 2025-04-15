using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ErrorManagement;

namespace PetFamily.Species.Contracts;
public interface ISpeciesContract
{
    Task<UnitResult<Error>> IsSpecieExistsById(Guid specieId, CancellationToken cancellationToken = default);
    Task<UnitResult<Error>> IsBreedExistsById(Guid breedId, CancellationToken cancellationToken = default);
}
