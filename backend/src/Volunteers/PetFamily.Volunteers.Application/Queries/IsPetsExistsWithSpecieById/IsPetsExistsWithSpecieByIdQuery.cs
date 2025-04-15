using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Queries.IsPetsExistsWithSpecieById;
public record IsPetsExistsWithSpecieByIdQuery(Guid SpecieId) : IQuery;