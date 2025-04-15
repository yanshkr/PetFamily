using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.Core.Models;
using PetFamily.Species.Contracts.Dtos.Breed;

namespace PetFamily.Species.Application.Queries.GetAllBreedsBySpecieId;
public class GetAllBreedsBySpecieIdHandler
    : IQueryHandler<PagedList<BreedDto>, GetAllBreedsBySpecieIdQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<GetAllBreedsBySpecieIdHandler> _logger;
    public GetAllBreedsBySpecieIdHandler(
        IReadDbContext readDbContext,
        ILogger<GetAllBreedsBySpecieIdHandler> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<PagedList<BreedDto>> HandleAsync(
        GetAllBreedsBySpecieIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var breedsQuery = _readDbContext.Breeds;

        breedsQuery = breedsQuery
            .Where(b => b.SpecieId == query.SpecieId);

        breedsQuery = breedsQuery.WhereIf(
                !string.IsNullOrWhiteSpace(query.Name),
                x => x.Name.Contains(query.Name!));

        return await breedsQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}