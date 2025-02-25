using PetFamily.Application.Dtos;
using PetFamily.Application.Features.Commands.Volunteers.UpdatePetMainInfo;
using PetFamily.Domain.Volunteers.Enums;

namespace PetFamily.Api.Controllers.Volunteers.Requests;
public record UpdatePetMainInfoRequest(
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
        bool IsSterilized)
{
    public UpdatePetMainInfoCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId,
            petId,
            Name,
            Description,
            Type,
            BreedId,
            SpecieId,
            Color,
            HealthInfo,
            Address,
            PhoneNumber,
            Weight,
            Height,
            BirthDate,
            IsSterilized);
}