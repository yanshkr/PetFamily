using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extensions;
using PetFamily.Application.Features.Commands.Volunteers.AddPet;
using PetFamily.Application.Models;

namespace PetFamily.Application.Features.Queries.Species.GetAllSpeciesWithPagination;
public class GetAllSpeciesWithPaginationHandler
    : IQueryHandler<PagedList<SpecieDto>, GetAllSpeciesWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<AddPetHandler> _logger;
    public GetAllSpeciesWithPaginationHandler(
        IReadDbContext readDbContext,
        ILogger<AddPetHandler> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<PagedList<SpecieDto>> HandleAsync(
        GetAllSpeciesWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var volunteersQuery = _readDbContext.Species;

        volunteersQuery = volunteersQuery.WhereIf(
                !string.IsNullOrWhiteSpace(query.Name),
                x => x.Name.Contains(query.Name!));

        return await volunteersQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}