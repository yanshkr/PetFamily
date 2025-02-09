namespace PetFamily.Application.Features.Volunteers.UpdateVolunteerMainInfo;
public record UpdateVolunteerCommand(
    Guid Id,
    string FirstName,
    string MiddleName,
    string Surname,
    string Email,
    string Description,
    int Experience,
    string PhoneNumber);
