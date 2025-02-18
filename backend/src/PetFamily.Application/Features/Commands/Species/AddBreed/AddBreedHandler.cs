using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Species.Entities;
using PetFamily.Domain.Species.Ids;

namespace PetFamily.Application.Features.Commands.Species.AddBreed;
public class AddBreedHandler
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddBreedCommand> _validator;
    private readonly ILogger<AddBreedHandler> _logger;
    public AddBreedHandler(
        ISpeciesRepository speciesRepository,
        IUnitOfWork unitOfWork,
        IValidator<AddBreedCommand> validator,
        ILogger<AddBreedHandler> logger)
    {
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<PetBreedId, ErrorList>> HandleAsync(
        AddBreedCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var petSpecieResult = await _speciesRepository.GetByIdAsync(command.SpecieId, cancellationToken);
        if (petSpecieResult.IsFailure)
            return petSpecieResult.Error.ToErrorList();

        var breedId = PetBreedId.NewPetBreedId();
        var name = Name.Create(command.Name).Value;

        var breedResult = PetBreed.Create(breedId, name);
        if (breedResult.IsFailure)
            return breedResult.Error.ToErrorList();

        petSpecieResult.Value.AddBreed(breedResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return breedId;
    }
}
