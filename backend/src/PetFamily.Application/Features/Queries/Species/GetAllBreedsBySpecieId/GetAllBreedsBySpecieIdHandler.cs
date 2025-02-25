using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extensions;
using PetFamily.Application.Features.Commands.Volunteers.AddPet;
using PetFamily.Application.Models;

namespace PetFamily.Application.Features.Queries.Species.GetAllBreedsBySpecieId;
public class GetAllBreedsBySpecieIdHandler
    : IQueryHandler<PagedList<BreedDto>, GetAllBreedsBySpecieIdQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<AddPetHandler> _logger;
    public GetAllBreedsBySpecieIdHandler(
        IReadDbContext readDbContext,
        ILogger<AddPetHandler> logger)
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