using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.Features.Volunteers.UpdateVolunteer;
public record UpdateVolunteerRequest(
    VolunteerId Id,
    UpdateVolunteerDto Dto
    );

public record UpdateVolunteerDto(
    string FirstName,
    string MiddleName,
    string Surname,
    string Email,
    string Description,
    int Experience,
    string PhoneNumber
    );