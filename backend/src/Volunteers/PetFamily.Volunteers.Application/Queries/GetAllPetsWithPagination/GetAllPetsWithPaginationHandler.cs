using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.Core.Models;
using PetFamily.Volunteers.Contracts.Dtos.Pet;
using System.Linq.Expressions;

namespace PetFamily.Volunteers.Application.Queries.GetAllPetsWithPagination;
public class GetAllPetsWithPaginationHandler
    : IQueryHandler<PagedList<PetDto>, GetAllPetsWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<GetAllPetsWithPaginationHandler> _logger;
    public GetAllPetsWithPaginationHandler(
        IReadDbContext readDbContext,
        ILogger<GetAllPetsWithPaginationHandler> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<PagedList<PetDto>> HandleAsync(
        GetAllPetsWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var petsQuery = _readDbContext.Pets;

        Expression<Func<PetDto, object>> orderExpression = query.SortBy?.ToLower() switch
        {
            "name" => pet => pet.Name,
            "age" => pet => pet.BirthDate.Year - DateTime.UtcNow.Year,
            "specie" => pet => pet.SpecieId,
            "breed" => pet => pet.BreedId,
            "country" => pet => pet.Address.Country,
            "state" => pet => pet.Address.State,
            "city" => pet => pet.Address.City,
            _ => pet => pet.Id,
        };

        petsQuery = query.SortDirection?.ToLower() switch
        {
            "asc" => petsQuery.OrderBy(orderExpression),
            "desc" => petsQuery.OrderByDescending(orderExpression),
            _ => petsQuery
        };

        petsQuery = petsQuery.WhereIf(
                !string.IsNullOrWhiteSpace(query.Name),
                x => x.Name.Contains(query.Name!));

        petsQuery = petsQuery.WhereIf(
                query.Age is not null,
                x => x.BirthDate.Year - DateTime.UtcNow.Year < query.Age);

        petsQuery = petsQuery.WhereIf(
                query.SpecieId is not null,
                x => x.SpecieId == query.SpecieId);

        petsQuery = petsQuery.WhereIf(
                query.BreedId is not null,
                x => x.BreedId == query.BreedId);

        petsQuery = petsQuery.WhereIf(
                !string.IsNullOrWhiteSpace(query.Country),
                x => x.Address.Country.Contains(query.Country!));

        petsQuery = petsQuery.WhereIf(
                !string.IsNullOrWhiteSpace(query.State),
                x => x.Address.Country.Contains(query.State!));

        petsQuery = petsQuery.WhereIf(
                !string.IsNullOrWhiteSpace(query.City),
                x => x.Address.Country.Contains(query.City!));

        return await petsQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}
