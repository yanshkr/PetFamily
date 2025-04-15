using PetFamily.Core.Abstraction;

namespace PetFamily.Species.Application.Commands.CreateSpecie;
public record CreateSpecieCommand(string Name) : ICommand;