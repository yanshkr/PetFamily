using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.Core.Models;
using PetFamily.Species.Contracts.Dtos.Specie;

namespace PetFamily.Species.Application.Queries.GetAllSpeciesWithPagination;
public class GetAllSpeciesWithPaginationHandler
    : IQueryHandler<PagedList<SpecieDto>, GetAllSpeciesWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<GetAllSpeciesWithPaginationHandler> _logger;
    public GetAllSpeciesWithPaginationHandler(
        IReadDbContext readDbContext,
        ILogger<GetAllSpeciesWithPaginationHandler> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<PagedList<SpecieDto>> HandleAsync(
        GetAllSpeciesWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var speciesQuery = _readDbContext.Species;

        speciesQuery = speciesQuery.WhereIf(
                !string.IsNullOrWhiteSpace(query.Name),
                x => x.Name.Contains(query.Name!));

        return await speciesQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}