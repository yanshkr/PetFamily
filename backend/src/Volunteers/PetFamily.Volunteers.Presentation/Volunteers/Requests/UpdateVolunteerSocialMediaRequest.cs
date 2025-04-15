using PetFamily.Volunteers.Application.Commands.UpdateVolunteerSocialMedia;
using PetFamily.Volunteers.Contracts.Dtos.Volunteer;

namespace PetFamily.Volunteers.Presentation.Volunteers.Requests;
public record UpdateVolunteerSocialMediaRequest(
    IEnumerable<SocialMediaDto> SocialMedias)
{
    public UpdateVolunteerSocialMediaCommand ToCommand(Guid Id)
        => new(Id, SocialMedias);
}
