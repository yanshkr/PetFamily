using PetFamily.Core.Abstraction;
using PetFamily.Volunteers.Domain.Enums;

namespace PetFamily.Volunteers.Application.Commands.UpdatePetStatus;
public record UpdatePetStatusCommand(
    Guid VolunteerId,
    Guid PetId,
    PetStatus Status) : ICommand;
