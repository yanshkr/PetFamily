using PetFamily.Application.Abstraction;
using PetFamily.Application.Dtos;
using PetFamily.Domain.Volunteers.Enums;

namespace PetFamily.Application.Features.Commands.Volunteers.UpdatePetMainInfo;
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