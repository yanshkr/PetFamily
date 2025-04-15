using PetFamily.Core.Abstraction;

namespace PetFamily.Species.Application.Queries.GetAllSpeciesWithPagination;
public record GetAllSpeciesWithPaginationQuery(
    string? Name,
    int Page,
    int PageSize) : IQuery;
