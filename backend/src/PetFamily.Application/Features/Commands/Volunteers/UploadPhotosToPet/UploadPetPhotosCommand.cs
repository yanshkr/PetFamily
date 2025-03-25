using PetFamily.Application.Abstraction;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.Features.Commands.Volunteers.UploadPhotosToPet;
public record UploadPetPhotosCommand(
    Guid VolunteerId, 
    Guid PetId, 
    IEnumerable<UploadFileDto> Photos) : ICommand;