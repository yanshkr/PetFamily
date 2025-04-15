
using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Commands.DeletePet;
public record DeletePetCommand(
    Guid VolunteerId,
    Guid PetId,
    bool IsSoftDelete) : ICommand;
