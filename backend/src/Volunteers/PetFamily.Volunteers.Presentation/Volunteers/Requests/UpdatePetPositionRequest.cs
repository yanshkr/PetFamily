using PetFamily.Volunteers.Application.Commands.UpdatePetPosition;

namespace PetFamily.Volunteers.Presentation.Volunteers.Requests;
public record UpdatePetPositionRequest(int Position)
{
    public UpdatePetPositionCommand ToCommand(Guid volunteerId, Guid petId)
        => new(volunteerId, petId, Position);
}