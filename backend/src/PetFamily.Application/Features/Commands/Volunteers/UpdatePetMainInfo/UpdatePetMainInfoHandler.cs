using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Features.Commands.Species;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Volunteers.Ids;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Application.Features.Commands.Volunteers.UpdatePetMainInfo;
public class UpdatePetMainInfoHandler
    : ICommandHandler<PetId, UpdatePetMainInfoCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdatePetMainInfoCommand> _validator;
    private readonly ILogger<UpdatePetMainInfoHandler> _logger;
    public UpdatePetMainInfoHandler(
        IVolunteersRepository volunteersRepository,
        ISpeciesRepository speciesRepository,
        IUnitOfWork unitOfWork,
        IValidator<UpdatePetMainInfoCommand> validator,
        ILogger<UpdatePetMainInfoHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<PetId, ErrorList>> HandleAsync(
        UpdatePetMainInfoCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var volunteerResult = await _volunteersRepository.GetByIdAsync(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petResult = volunteerResult.Value.GetPetById(command.PetId);
        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();

        var speciesResult = await _speciesRepository.GetByIdAsync(command.SpecieId, cancellationToken);
        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var breedResult = speciesResult.Value.GetBreed(command.BreedId);
        if (breedResult.IsFailure)
            return breedResult.Error.ToErrorList();

        var name = Name.Create(command.Name).Value;
        var description = Description.Create(command.Description).Value;
        var color = Color.Create(command.Color).Value;
        var healthInfo = HealthInfo.Create(command.HealthInfo).Value;
        var address = Address.Create(command.Address.Country, command.Address.State, command.Address.City, command.Address.Street, command.Address.ZipCode).Value;
        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
        var weight = WeightMeasurement.CreateFromGrams(command.Weight).Value;
        var height = HeightMeasurement.CreateFromCentimeters(command.Height).Value;

        petResult.Value.UpdateMainInfo(
            name,
            description,
            command.Type,
            breedResult.Value,
            speciesResult.Value,
            color,
            healthInfo,
            address,
            phoneNumber,
            weight,
            height,
            command.BirthDate,
            command.IsSterilized);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return petResult.Value.Id;
    }
}
