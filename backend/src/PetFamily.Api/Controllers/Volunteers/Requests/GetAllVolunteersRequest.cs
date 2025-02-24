using PetFamily.Application.Features.Queries.Volunteers.GetAllVolunteersWithPagination;

namespace PetFamily.Api.Controllers.Volunteers.Requests;
public record GetAllVolunteersRequest(
    string? FisrtName,
    string? LastName,
    string? Email,
    string? PhoneNumber,
    int Page,
    int PageSize)
{
    public GetAllVolunteersWithPaginationQuery ToQuery()
    {
        return new GetAllVolunteersWithPaginationQuery(
            FisrtName,
            LastName,
            Email,
            PhoneNumber,
            Page,
            PageSize);
    }
};