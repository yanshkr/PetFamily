using PetFamily.Application.Dtos;
using PetFamily.Domain.Species.Ids;
using PetFamily.Domain.Volunteers.Enums;
using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.Features.Volunteers.AddPet;
public record AddPetCommand(
        VolunteerId VolunteerId,
        string Name,
        string Description,
        PetType Type,
        PetSpecieId SpecieId,
        PetBreedId BreedId,
        string Color,
        string HealthInfo,
        AddressDto Address,
        string PhoneNumber,
        int Weight,
        int Height,
        DateTime BirthDate,
        bool IsSterilized,
        PetStatus Status);