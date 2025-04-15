using PetFamily.Core.Abstraction;

namespace PetFamily.Species.Application.Queries.GetSpecieById;
public record GetSpecieByIdQuery(Guid SpecieId) : IQuery;
