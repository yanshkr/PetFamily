using PetFamily.Application.Abstraction;

namespace PetFamily.Application.Features.Queries.Species.GetAllSpeciesWithPagination;
public record GetAllSpeciesWithPaginationQuery(
    string? Name,
    int Page,
    int PageSize) : IQuery;
