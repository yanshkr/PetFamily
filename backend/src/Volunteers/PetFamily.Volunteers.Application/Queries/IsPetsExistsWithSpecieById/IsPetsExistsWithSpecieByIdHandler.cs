using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Queries.IsPetsExistsWithSpecieById;
public class IsPetsExistsWithSpecieByIdHandler
    : IQueryHandler<Result<bool>, IsPetsExistsWithSpecieByIdQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<IsPetsExistsWithSpecieByIdHandler> _logger;
    public IsPetsExistsWithSpecieByIdHandler(
        IReadDbContext readDbContext,
        ILogger<IsPetsExistsWithSpecieByIdHandler> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<Result<bool>> HandleAsync(
        IsPetsExistsWithSpecieByIdQuery query,
        CancellationToken cancellationToken = default)
    {

        return await _readDbContext.Pets
            .AnyAsync(v => v.SpecieId == query.SpecieId, cancellationToken);
    }
}
