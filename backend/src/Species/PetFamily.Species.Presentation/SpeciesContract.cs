using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.Species.Application.Queries.GetBreedById;
using PetFamily.Species.Application.Queries.GetSpecieById;
using PetFamily.Species.Contracts;

namespace PetFamily.Species.Presentation;
public class SpeciesContract : ISpeciesContract
{
    private readonly GetSpecieByIdHandler _getSpecieByIdHandler;
    private readonly GetBreedByIdHandler _getBreedByIdHandler;

    public SpeciesContract(
        GetSpecieByIdHandler getSpecieByIdHandler,
        GetBreedByIdHandler getBreedByIdHandler)
    {
        _getBreedByIdHandler = getBreedByIdHandler;
        _getSpecieByIdHandler = getSpecieByIdHandler;
    }

    public async Task<UnitResult<Error>> IsSpecieExistsById(Guid specieId, CancellationToken cancellationToken = default)
    {
        var specieDto = await _getSpecieByIdHandler.HandleAsync(new GetSpecieByIdQuery(specieId), cancellationToken);

        if (specieDto.IsFailure)
            return Errors.General.NotFound(specieId);

        return UnitResult.Success<Error>();
    }

    public async Task<UnitResult<Error>> IsBreedExistsById(Guid breedId, CancellationToken cancellationToken = default)
    {
        var breedDto = await _getBreedByIdHandler.HandleAsync(new GetBreedByIdQuery(breedId), cancellationToken);

        if (breedDto.IsFailure)
            return Errors.General.NotFound(breedId);

        return UnitResult.Success<Error>();
    }
}
