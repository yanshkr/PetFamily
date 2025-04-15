using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Queries.IsPetsExistsWithBreedById;
public record IsPetsExistsWithBreedByIdQuery(Guid BreedId) : IQuery;
