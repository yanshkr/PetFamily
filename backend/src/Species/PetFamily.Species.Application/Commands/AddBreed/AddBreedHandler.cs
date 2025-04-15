using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Database;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.SharedKernel.Structs;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Species.Domain.Entities;
using PetFamily.Species.Domain.Ids;

namespace PetFamily.Species.Application.Commands.AddBreed;
public class AddBreedHandler
    : ICommandHandler<PetBreedId, AddBreedCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddBreedCommand> _validator;
    private readonly ILogger<AddBreedHandler> _logger;
    public AddBreedHandler(
        ISpeciesRepository speciesRepository,
        [FromKeyedServices(UnitOfWorkSelector.Species)] IUnitOfWork unitOfWork,
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
