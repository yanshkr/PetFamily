using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.Features.Volunteers.DeleteVolunteer;
public record DeleteVolunteerCommand(
    VolunteerId Id,
    bool IsSoftDelete);
