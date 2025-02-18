using PetFamily.Application.Abstraction;

namespace PetFamily.Application.Features.Commands.Volunteers.DeletePhotosAtPet;
public record DeletePetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<string> Photos) : ICommand;