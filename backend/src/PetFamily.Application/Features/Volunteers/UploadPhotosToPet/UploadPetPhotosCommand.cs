using PetFamily.Application.Dtos;
using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.Features.Volunteers.UploadPhotoToPet;
public record UploadPetPhotosCommand(
    VolunteerId VolunteerId,
    PetId PetId,
    IEnumerable<UploadFileDto> Photos);
