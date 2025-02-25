using PetFamily.Application.Features.Commands.Volunteers.UpdatePetMainPhoto;

namespace PetFamily.Api.Controllers.Volunteers.Requests;

public record UpdatePetMainPhotoRequest(string FileName)
{
    public UpdatePetMainPhotoCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId, petId, FileName);
}