using PetFamily.Application.Abstraction;

namespace PetFamily.Application.Features.Commands.Species.Create;
public record CreateSpecieCommand(string Name) : ICommand;