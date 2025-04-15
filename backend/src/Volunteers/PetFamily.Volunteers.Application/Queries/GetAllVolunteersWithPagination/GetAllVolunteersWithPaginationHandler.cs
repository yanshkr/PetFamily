using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.Core.Models;
using PetFamily.Volunteers.Application.Commands.AddPet;
using PetFamily.Volunteers.Contracts.Dtos.Volunteer;

namespace PetFamily.Volunteers.Application.Queries.GetAllVolunteersWithPagination;
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
