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
using PetFamily.Species.Contracts;
using PetFamily.Volunteers.Domain.Entities;
using PetFamily.Volunteers.Domain.Ids;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.AddPet;
public class AddPetHandler
    : ICommandHandler<PetId, AddPetCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ISpeciesContract _speciesContract;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddPetCommand> _validator;
    private readonly ILogger<AddPetHandler> _logger;
    public AddPetHandler(
        IVolunteersRepository volunteersRepository,
        ISpeciesContract speciesContract,
        [FromKeyedServices(UnitOfWorkSelector.Volunteers)] IUnitOfWork unitOfWork,
        IValidator<AddPetCommand> validator,
        ILogger<AddPetHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _speciesContract = speciesContract;
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

        var speciesResult = await _speciesContract.IsSpecieExistsById(command.SpecieId, cancellationToken);
        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var breedResult = await _speciesContract.IsBreedExistsById(command.BreedId, cancellationToken);
        if (breedResult.IsFailure)
            return breedResult.Error.ToErrorList();

        var name = Name.Create(command.Name).Value;
        var description = Description.Create(command.Description).Value;
        var classification = PetClassification.Create(command.BreedId, command.SpecieId).Value;
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
            classification,
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