using PetFamily.Application.Features.Volunteers.UpdateVolunteerMainInfo;
using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Api.Controllers.Volunteers.Requests;

public record UpdateVolunteerRequest(
    string FirstName,
    string MiddleName,
    string Surname,
    string Email,
    string Description,
    int Experience,
    string PhoneNumber)
{
    public UpdateVolunteerCommand ToCommand(VolunteerId id) => new(
        id,
        FirstName,
        MiddleName,
        Surname,
        Email,
        Description,
        Experience,
        PhoneNumber);
}
