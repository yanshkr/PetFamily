namespace PetFamily.Application.Features.Volunteers.UpdateVolunteerMainInfo.Contracts;

public record UpdateVolunteerDto(
    string FirstName,
    string MiddleName,
    string Surname,
    string Email,
    string Description,
    int Experience,
    string PhoneNumber
    );