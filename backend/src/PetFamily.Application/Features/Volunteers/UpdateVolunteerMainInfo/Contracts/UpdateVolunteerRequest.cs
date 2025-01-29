using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.Features.Volunteers.UpdateVolunteerMainInfo.Contracts;
public record UpdateVolunteerRequest(
    VolunteerId Id,
    UpdateVolunteerDto Dto
    );
