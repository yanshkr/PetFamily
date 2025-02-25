using PetFamily.Application.Abstraction;
using PetFamily.Domain.Volunteers.Enums;

namespace PetFamily.Application.Features.Commands.Volunteers.UpdatePetStatus;
public record UpdatePetStatusCommand(
    Guid VolunteerId,
    Guid PetId,
    PetStatus Status) : ICommand;
