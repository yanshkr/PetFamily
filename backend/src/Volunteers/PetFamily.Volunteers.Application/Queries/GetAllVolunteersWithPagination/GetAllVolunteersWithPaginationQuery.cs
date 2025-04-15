using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Queries.GetAllVolunteersWithPagination;
public record GetAllVolunteersWithPaginationQuery(
    string? FisrtName,
    string? LastName,
    string? Email,
    string? PhoneNumber,
    int Page,
    int PageSize) : IQuery;
