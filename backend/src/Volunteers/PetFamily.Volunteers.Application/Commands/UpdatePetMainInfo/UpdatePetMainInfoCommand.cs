using PetFamily.Core.Abstraction;
using PetFamily.Volunteers.Contracts.Dtos.Pet;
using PetFamily.Volunteers.Domain.Enums;

namespace PetFamily.Volunteers.Application.Commands.UpdatePetMainInfo;
public record UpdatePetMainInfoCommand(
        Guid VolunteerId,
        Guid PetId,
        string Name,
        string Description,
        PetType Type,
        Guid BreedId,
        Guid SpecieId,
        string Color,
        string HealthInfo,
        AddressDto Address,
        string PhoneNumber,
        int Weight,
        int Height,
        DateTime BirthDate,
        bool IsSterilized) : ICommand;