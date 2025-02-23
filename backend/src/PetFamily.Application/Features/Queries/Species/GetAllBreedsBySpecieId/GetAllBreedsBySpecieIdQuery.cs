using PetFamily.Application.Abstraction;

namespace PetFamily.Application.Features.Queries.Species.GetAllBreedsBySpecieId;
public record GetAllBreedsBySpecieIdQuery(
    Guid SpecieId,
    string? Name,
    int Page,
    int PageSize) : IQuery;