using PetFamily.Core.Abstraction;

namespace PetFamily.Species.Application.Commands.DeleteSpecie;
public record DeleteSpecieCommand(Guid Id) : ICommand;
