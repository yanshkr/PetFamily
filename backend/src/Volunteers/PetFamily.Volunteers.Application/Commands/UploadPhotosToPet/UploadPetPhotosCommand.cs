using PetFamily.Core.Abstraction;
using PetFamily.Volunteers.Contracts.Dtos.Pet;

namespace PetFamily.Volunteers.Application.Commands.UploadPhotosToPet;
public record UploadPetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<UploadFileDto> Photos) : ICommand;