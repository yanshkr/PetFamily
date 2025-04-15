using PetFamily.Volunteers.Application.Commands.UpdateVolunteerMainInfo;

namespace PetFamily.Volunteers.Presentation.Volunteers.Requests;

public record UpdateVolunteerRequest(
    string FirstName,
    string MiddleName,
    string Surname,
    string Email,
    string Description,
    int Experience,
    string PhoneNumber)
{
    public UpdateVolunteerCommand ToCommand(Guid id) => new(
        id,
        FirstName,
        MiddleName,
        Surname,
        Email,
        Description,
        Experience,
        PhoneNumber);
}
