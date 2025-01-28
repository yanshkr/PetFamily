using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.Features.Volunteers.UpdateVolunteerSocialMedia;
public record UpdateVolunteerSocialMediaRequest(
    VolunteerId Id,
    UpdateVolunteerSocialMediaDto Dto
    );

public record UpdateVolunteerSocialMediaDto(
    IEnumerable<SocialMediaRaw> SocialMedias
    );

public record SocialMediaRaw(
    string Url,
    string Type
    );
