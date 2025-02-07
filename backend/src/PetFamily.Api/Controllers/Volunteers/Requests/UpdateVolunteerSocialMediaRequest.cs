using PetFamily.Application.Dtos;
using PetFamily.Application.Features.Volunteers.UpdateVolunteerSocialMedia;
using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Api.Controllers.Volunteers.Requests;
public record UpdateVolunteerSocialMediaRequest(
    IEnumerable<SocialMediaDto> SocialMedias)
{
    public UpdateVolunteerSocialMediaCommand ToCommand(VolunteerId Id)
        => new(Id, SocialMedias);
}
