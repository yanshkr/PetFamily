using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Species;
using PetFamily.Domain.Species.Ids;

namespace PetFamily.Application.Features.Commands.Species.Create;
public class CreateSpecieHandler
    : ICommandHandler<PetSpecieId, CreateSpecieCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateSpecieCommand> _validator;
    private readonly ILogger<CreateSpecieHandler> _logger;
    public CreateSpecieHandler(
        ISpeciesRepository speciesRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateSpecieCommand> validator,
        ILogger<CreateSpecieHandler> logger)
    {
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<PetSpecieId, ErrorList>> HandleAsync(
        CreateSpecieCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var petSpecieId = PetSpecieId.NewPetSpecieId();
        var name = Name.Create(command.Name).Value;
        var petSpecie = PetSpecie.Create(petSpecieId, name).Value;

        await _speciesRepository.AddAsync(petSpecie, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return petSpecieId;
    }
}
