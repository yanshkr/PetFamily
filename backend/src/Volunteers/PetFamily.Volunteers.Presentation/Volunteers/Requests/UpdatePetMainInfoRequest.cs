using PetFamily.Volunteers.Application.Commands.UpdatePetMainInfo;
using PetFamily.Volunteers.Contracts.Dtos.Pet;
using PetFamily.Volunteers.Domain.Enums;

namespace PetFamily.Volunteers.Presentation.Volunteers.Requests;
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