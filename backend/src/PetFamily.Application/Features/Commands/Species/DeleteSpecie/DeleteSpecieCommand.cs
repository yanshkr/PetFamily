using PetFamily.Application.Abstraction;

namespace PetFamily.Application.Features.Commands.Species.Delete;
public record DeleteSpecieCommand(Guid Id) : ICommand;
