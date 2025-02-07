using PetFamily.Application.Dtos;

namespace PetFamily.Application.Features.Volunteers.CreateVolunteer;
public record CreateVolunteerCommand(
    FullNameDto FullName,
    string Email,
    string Description,
    int Experience,
    string PhoneNumber);