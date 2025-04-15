using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.Species.Contracts.Dtos.Breed;

namespace PetFamily.Species.Application.Queries.GetBreedById;
public class GetBreedByIdHandler
    : IQueryHandler<Result<BreedDto, Error>, GetBreedByIdQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<GetBreedByIdHandler> _logger;
    public GetBreedByIdHandler(
        IReadDbContext readDbContext,
        ILogger<GetBreedByIdHandler> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<Result<BreedDto, Error>> HandleAsync(
        GetBreedByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var breedDto = await _readDbContext.Breeds
            .FirstOrDefaultAsync(b => b.Id == query.BreedId, cancellationToken);

        if (breedDto is null)
            return Errors.General.NotFound(query.BreedId);

        return breedDto;
    }
}