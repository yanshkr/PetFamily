using PetFamily.Application.Abstraction;

namespace PetFamily.Application.Features.Queries.GetAllVolunteersWithPagination;
public record GetAllVolunteersWithPaginationQuery(
    string? FisrtName,
    string? LastName,
    string? Email,
    string? PhoneNumber,
    int Page,
    int PageSize) : IQuery;
