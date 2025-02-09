using PetFamily.Application.Dtos;

namespace PetFamily.Application.Features.Volunteers.UpdateVolunteerSocialMedia;
public record UpdateVolunteerSocialMediaCommand(
    Guid Id,
    IEnumerable<SocialMediaDto> SocialMedias);