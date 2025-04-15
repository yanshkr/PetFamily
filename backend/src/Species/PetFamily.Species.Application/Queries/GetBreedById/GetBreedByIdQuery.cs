using PetFamily.Core.Abstraction;

namespace PetFamily.Species.Application.Queries.GetBreedById;
public record GetBreedByIdQuery(Guid BreedId) : IQuery;
