using PetFamily.Domain.Species.Ids;

namespace PetFamily.Application.Features.Species.AddBreed;
public record AddBreedCommand(PetSpecieId SpecieId, string Name);