using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Queries.IsPetsExistsWithBreedById;
public class IsPetsExistsWithBreedByIdHandler
    : IQueryHandler<Result<bool>, IsPetsExistsWithBreedByIdQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<IsPetsExistsWithBreedByIdHandler> _logger;
    public IsPetsExistsWithBreedByIdHandler(
        IReadDbContext readDbContext,
        ILogger<IsPetsExistsWithBreedByIdHandler> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<Result<bool>> HandleAsync(
        IsPetsExistsWithBreedByIdQuery query,
        CancellationToken cancellationToken = default)
    {

        return await _readDbContext.Pets
            .AnyAsync(v => v.BreedId == query.BreedId, cancellationToken); ;
    }
}
