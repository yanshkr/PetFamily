using PetFamily.Core.Abstraction;
using PetFamily.Volunteers.Contracts.Dtos.Volunteer;

namespace PetFamily.Volunteers.Application.Commands.UpdateVolunteerSocialMedia;
public record UpdateVolunteerSocialMediaCommand(
    Guid Id,
    IEnumerable<SocialMediaDto> SocialMedias) : ICommand;