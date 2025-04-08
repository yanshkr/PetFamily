using PetFamily.Application.Features.Queries.Pets.GetAllPetsWithPagination;

namespace PetFamily.Api.Controllers.Volunteers.Requests;
public record GetAllPetsRequest(
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
    int PageSize)
{
    public GetAllPetsWithPaginationQuery ToQuery()
    {
        return new GetAllPetsWithPaginationQuery(
            VolunteerId,
            Name,
            Age,
            SpecieId,
            BreedId,
            Country,
            State,
            City,
            SortBy,
            SortDirection,
            Page,
            PageSize);
    }
}