using PetFamily.Application.Dtos;
using PetFamily.Application.Features.Commands.Volunteers.UpdateVolunteerSocialMedia;

namespace PetFamily.Api.Controllers.Volunteers.Requests;
public record UpdateVolunteerSocialMediaRequest(
    IEnumerable<SocialMediaDto> SocialMedias)
{
    public UpdateVolunteerSocialMediaCommand ToCommand(Guid Id)
        => new(Id, SocialMedias);
}
