using PetFamily.Application.Features.Volunteers.UpdatePetPosition;

namespace PetFamily.Api.Controllers.Volunteers.Requests;
public record UpdatePetPositionRequest(int Position)
{
    public UpdatePetPositionCommand ToCommand(Guid volunteerId, Guid petId) 
        => new(volunteerId, petId, Position);
}