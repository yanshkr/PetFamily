using PetFamily.Core.Abstraction;
using PetFamily.Volunteers.Contracts.Dtos.Pet;
using PetFamily.Volunteers.Domain.Enums;

namespace PetFamily.Volunteers.Application.Commands.AddPet;
public record AddPetCommand(
        Guid VolunteerId,
        string Name,
        string Description,
        PetType Type,
        Guid SpecieId,
        Guid BreedId,
        string Color,
        string HealthInfo,
        AddressDto Address,
        string PhoneNumber,
        int Weight,
        int Height,
        DateTime BirthDate,
        bool IsSterilized,
        PetStatus Status) : ICommand;