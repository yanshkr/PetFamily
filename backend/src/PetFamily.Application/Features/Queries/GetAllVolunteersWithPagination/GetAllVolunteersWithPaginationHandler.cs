using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extensions;
using PetFamily.Application.Features.Commands.Volunteers.AddPet;
using PetFamily.Application.Models;

namespace PetFamily.Application.Features.Queries.GetAllVolunteersWithPagination;
public class GetAllVolunteersWithPaginationHandler 
    : IQueryHandler<PagedList<VolunteerDto>, GetAllVolunteersWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<AddPetHandler> _logger;
    public GetAllVolunteersWithPaginationHandler(
        IReadDbContext readDbContext,
        ILogger<AddPetHandler> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<PagedList<VolunteerDto>> HandleAsync(
        GetAllVolunteersWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var volunteersQuery = _readDbContext.Volunteers;

        volunteersQuery = volunteersQuery.WhereIf(
                !string.IsNullOrWhiteSpace(query.FisrtName),
                x => x.FirstName.Contains(query.FisrtName!));

        volunteersQuery = volunteersQuery.WhereIf(
                !string.IsNullOrWhiteSpace(query.LastName),
                x => x.LastName.Contains(query.LastName!));

        volunteersQuery = volunteersQuery.WhereIf(
                !string.IsNullOrWhiteSpace(query.Email),
                x => x.Email.Contains(query.Email!));

        volunteersQuery = volunteersQuery.WhereIf(
                !string.IsNullOrWhiteSpace(query.PhoneNumber),
                x => x.PhoneNumber.Contains(query.PhoneNumber!));

        return await volunteersQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}
