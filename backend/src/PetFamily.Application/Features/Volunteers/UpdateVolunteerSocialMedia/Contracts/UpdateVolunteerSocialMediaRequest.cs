using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.Features.Volunteers.UpdateVolunteerSocialMedia.Contracts;
public record UpdateVolunteerSocialMediaRequest(
    VolunteerId Id,
    UpdateVolunteerSocialMediaDto Dto
    );