using PetFamily.Application.Abstraction;

namespace PetFamily.Application.Features.Commands.Species.DeleteBreed;
public record DeleteBreedCommand(Guid SpecieId, Guid BreedId) : ICommand;