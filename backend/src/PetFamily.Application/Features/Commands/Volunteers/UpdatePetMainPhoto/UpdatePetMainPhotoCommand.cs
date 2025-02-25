using PetFamily.Application.Abstraction;

namespace PetFamily.Application.Features.Commands.Volunteers.UpdatePetMainPhoto;
public record UpdatePetMainPhotoCommand(Guid VolunteerId, Guid PetId, string FileName) : ICommand;
