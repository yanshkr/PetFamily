using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ErrorManagement;

namespace PetFamily.Volunteers.Contracts;
public interface IVolunteersContract
{
    Task<UnitResult<Error>> IsPetsExistsWithSpecieById(Guid specieId, CancellationToken cancellationToken = default);
    Task<UnitResult<Error>> IsPetsExistsWithBreedById(Guid breedId, CancellationToken cancellationToken = default);
}
