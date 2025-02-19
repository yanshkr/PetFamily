using PetFamily.Application.Dtos;
using PetFamily.Application.Features.Commands.Volunteers.AddPet;
using PetFamily.Domain.Volunteers.Enums;

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
    public AddPetCommand ToCommand(Guid volunteerId)
        => new(
            volunteerId,
            Name,
            Description,
            Type,
            SpecieId,
            BreedId,
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