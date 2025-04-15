using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Commands.UpdatePetMainPhoto;
public record UpdatePetMainPhotoCommand(Guid VolunteerId, Guid PetId, string FileName) : ICommand;
