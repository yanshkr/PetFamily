using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.Volunteers.Application.Queries.IsPetsExistsWithBreedById;
using PetFamily.Volunteers.Application.Queries.IsPetsExistsWithSpecieById;
using PetFamily.Volunteers.Contracts;

namespace PetFamily.Volunteers.Presentation;
public class VolunteersContract : IVolunteersContract
{
    private readonly IsPetsExistsWithBreedByIdHandler _isPetsExistsWithBreedByIdHandler;
    private readonly IsPetsExistsWithSpecieByIdHandler _isPetsExistsWithSpecieByIdHandler;
    public VolunteersContract(
        IsPetsExistsWithBreedByIdHandler isPetsExistsWithBreedByIdHandler,
        IsPetsExistsWithSpecieByIdHandler isPetsExistsWithSpecieByIdHandler)
    {
        _isPetsExistsWithBreedByIdHandler = isPetsExistsWithBreedByIdHandler;
        _isPetsExistsWithSpecieByIdHandler = isPetsExistsWithSpecieByIdHandler;
    }

    public async Task<UnitResult<Error>> IsPetsExistsWithBreedById(Guid breedId, CancellationToken cancellationToken = default)
    {
        var petsExists = await _isPetsExistsWithBreedByIdHandler.HandleAsync(new IsPetsExistsWithBreedByIdQuery(breedId), cancellationToken);

        return petsExists.IsSuccess && petsExists.Value
            ? UnitResult.Success<Error>()
            : Errors.General.NotFound(breedId);
    }

    public async Task<UnitResult<Error>> IsPetsExistsWithSpecieById(Guid specieId, CancellationToken cancellationToken = default)
    {
        var petsExists = await _isPetsExistsWithSpecieByIdHandler.HandleAsync(new IsPetsExistsWithSpecieByIdQuery(specieId), cancellationToken);

        return petsExists.IsSuccess && petsExists.Value
            ? UnitResult.Success<Error>()
            : Errors.General.NotFound(specieId);
    }
}
