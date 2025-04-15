using PetFamily.Core.Abstraction;

namespace PetFamily.Species.Application.Commands.DeleteBreed;
public record DeleteBreedCommand(Guid SpecieId, Guid BreedId) : ICommand;