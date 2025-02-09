namespace PetFamily.Application.Features.Volunteers.DeletePhotosAtPet;
public record DeletePetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<string> Photos);