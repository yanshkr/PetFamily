using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Commands.UpdatePetPosition;
public record UpdatePetPositionCommand(Guid VolunteerId, Guid PetId, int Position) : ICommand;