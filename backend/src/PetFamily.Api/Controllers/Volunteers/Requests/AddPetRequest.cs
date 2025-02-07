using PetFamily.Application.Dtos;
using PetFamily.Application.Features.Volunteers.AddPet;
using PetFamily.Domain.Species.Ids;
using PetFamily.Domain.Volunteers.Enums;
using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Api.Controllers.Volunteers.Requests;

public record AddPetRequest(
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
        bool IsSterilized,
        PetStatus Status)
{
    public AddPetCommand ToCommand(VolunteerId volunteerId)
        => new(
            volunteerId,
            Name,
            Description,
            Type,
            PetSpecieId.FromGuid(SpecieId),
            PetBreedId.FromGuid(BreedId),
            Color,
            HealthInfo,
            Address,
            PhoneNumber,
            Weight,
            Height,
            BirthDate,
            IsSterilized,
            Status);
}