using PetFamily.Application.Abstraction;

namespace PetFamily.Application.Features.Commands.Volunteers.DeletePet;
public record DeletePetCommand(
    Guid VolunteerId,
    Guid PetId,
    bool IsSoftDelete) : ICommand;
