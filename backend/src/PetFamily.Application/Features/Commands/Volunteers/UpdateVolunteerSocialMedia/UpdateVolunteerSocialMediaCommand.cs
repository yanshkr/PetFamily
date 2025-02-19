using PetFamily.Application.Abstraction;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.Features.Commands.Volunteers.UpdateVolunteerSocialMedia;
public record UpdateVolunteerSocialMediaCommand(
    Guid Id,
    IEnumerable<SocialMediaDto> SocialMedias) : ICommand;