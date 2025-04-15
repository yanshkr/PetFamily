using PetFamily.Core.Abstraction;

namespace PetFamily.Species.Application.Queries.GetAllBreedsBySpecieId;
public record GetAllBreedsBySpecieIdQuery(
    Guid SpecieId,
    string? Name,
    int Page,
    int PageSize) : IQuery;