using PetFamily.Application.Dtos;

namespace PetFamily.Application.Features.Volunteers.UploadPhotoToPet;
public record UploadPetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<UploadFileDto> Photos);
