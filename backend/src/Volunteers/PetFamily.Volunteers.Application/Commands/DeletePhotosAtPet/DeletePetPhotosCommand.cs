using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Commands.DeletePhotosAtPet;
public record DeletePetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<string> Photos) : ICommand;