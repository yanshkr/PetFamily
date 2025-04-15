using PetFamily.Volunteers.Application.Commands.DeletePhotosAtPet;

namespace PetFamily.Volunteers.Presentation.Volunteers.Requests;
public record DeletePetPhotosRequest(IEnumerable<string> Photos)
{
    public DeletePetPhotosCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId, petId, Photos);
}