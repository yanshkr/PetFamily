using PetFamily.Application.Abstraction;

namespace PetFamily.Application.Features.Queries.Pets.GetAllPetsWithPagination;
public record GetAllPetsWithPaginationQuery(
    Guid? VolunteerId,
    string? Name,
    int? Age,
    Guid? SpecieId,
    Guid? BreedId,
    string? Country,
    string? State,
    string? City,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize) : IQuery;