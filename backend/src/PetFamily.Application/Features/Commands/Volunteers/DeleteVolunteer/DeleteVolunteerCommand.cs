using PetFamily.Application.Abstraction;

namespace PetFamily.Application.Features.Commands.Volunteers.DeleteVolunteer;
public record DeleteVolunteerCommand(
    Guid Id,
    bool IsSoftDelete) : ICommand;
