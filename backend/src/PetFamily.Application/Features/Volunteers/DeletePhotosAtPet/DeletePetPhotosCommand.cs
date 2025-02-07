using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.Features.Volunteers.DeletePhotosAtPet;
public record DeletePetPhotosCommand(
    VolunteerId VolunteerId,
    PetId PetId,
    IEnumerable<string> Photos);