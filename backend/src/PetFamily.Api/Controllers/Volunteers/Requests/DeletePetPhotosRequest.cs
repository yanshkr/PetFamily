using PetFamily.Application.Features.Volunteers.DeletePhotosAtPet;
using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Api.Controllers.Volunteers.Requests;
public record DeletePetPhotosRequest(IEnumerable<string> Photos)
{
    public DeletePetPhotosCommand ToCommand(VolunteerId volunteerId, PetId petId) =>
        new(volunteerId, petId, Photos);
}