using PetFamily.Application.Dtos;
using PetFamily.Application.Features.Volunteers.CreateVolunteer;

namespace PetFamily.Api.Controllers.Volunteers.Requests;

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