using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Features.Species;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Volunteers.Entities;
using PetFamily.Domain.Volunteers.Ids;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.AddPet;
public class AddPetHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddPetCommand> _validator;
    private readonly ILogger<AddPetHandler> _logger;
    public AddPetHandler(
        IVolunteersRepository volunteersRepository,
        ISpeciesRepository speciesRepository,
        IUnitOfWork unitOfWork,
        IValidator<AddPetCommand> validator,
        ILogger<AddPetHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<PetId, ErrorList>> HandleAsync(
        AddPetCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var volunteerResult = await _volunteersRepository.GetByIdAsync(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

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
        var address = Address.Create(
            command.Address.Country,
            command.Address.State,
            command.Address.City,
            command.Address.Street,
            command.Address.ZipCode).Value;
        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
        var weight = WeightMeasurement.CreateFromGrams(command.Weight).Value;
        var height = HeightMeasurement.CreateFromCentimeters(command.Height).Value;

        var pet = Pet.Create(
            PetId.NewPetId(),
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
            command.IsSterilized,
            command.Status);

        if (pet.IsFailure)
            return pet.Error.ToErrorList();

        volunteerResult.Value.AddPet(pet.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return pet.Value.Id;
    }
}