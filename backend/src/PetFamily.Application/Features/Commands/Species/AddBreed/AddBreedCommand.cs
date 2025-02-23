using PetFamily.Application.Abstraction;

namespace PetFamily.Application.Features.Commands.Species.AddBreed;
public record AddBreedCommand(Guid SpecieId, string Name) : ICommand;