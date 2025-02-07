using PetFamily.Application.Dtos;
using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.Features.Volunteers.UpdateVolunteerSocialMedia;
public record UpdateVolunteerSocialMediaCommand(
    VolunteerId Id,
    IEnumerable<SocialMediaDto> SocialMedias);