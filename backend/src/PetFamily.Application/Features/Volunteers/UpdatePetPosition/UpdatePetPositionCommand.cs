namespace PetFamily.Application.Features.Volunteers.UpdatePetPosition;
public record UpdatePetPositionCommand(Guid VolunteerId, Guid PetId, int Position);