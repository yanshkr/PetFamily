using PetFamily.Application.Abstraction;

namespace PetFamily.Application.Features.Commands.Volunteers.UpdatePetPosition;
public record UpdatePetPositionCommand(Guid VolunteerId, Guid PetId, int Position) : ICommand;