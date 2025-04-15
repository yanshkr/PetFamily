using PetFamily.Volunteers.Application.Commands.CreateVolunteer;
using PetFamily.Volunteers.Contracts.Dtos.Volunteer;

namespace PetFamily.Volunteers.Presentation.Volunteers.Requests;

public record CreateVolunteerRequest(
    FullNameDto FullName,
    string Email,
    string Description,
    int Experience,
    string PhoneNumber)
{
    public CreateVolunteerCommand ToCommand() => new(
        FullName,
        Email,
        Description,
        Experience,
        PhoneNumber);
}