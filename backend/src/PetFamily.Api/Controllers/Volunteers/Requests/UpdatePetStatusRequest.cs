using PetFamily.Application.Features.Commands.Volunteers.UpdatePetStatus;
using PetFamily.Domain.Volunteers.Enums;

namespace PetFamily.Api.Controllers.Volunteers.Requests;

public record UpdatePetStatusRequest(PetStatus Status)
{
    public UpdatePetStatusCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId,
            petId,
            Status);
}