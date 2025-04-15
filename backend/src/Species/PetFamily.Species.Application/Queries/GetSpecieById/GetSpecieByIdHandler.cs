using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.Species.Contracts.Dtos.Specie;

namespace PetFamily.Species.Application.Queries.GetSpecieById;
public class GetSpecieByIdHandler
    : IQueryHandler<Result<SpecieDto, Error>, GetSpecieByIdQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<GetSpecieByIdHandler> _logger;
    public GetSpecieByIdHandler(
        IReadDbContext readDbContext,
        ILogger<GetSpecieByIdHandler> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<Result<SpecieDto, Error>> HandleAsync(
        GetSpecieByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var specieDto = await _readDbContext.Species
            .FirstOrDefaultAsync(b => b.Id == query.SpecieId, cancellationToken);

        if (specieDto is null)
            return Errors.General.NotFound(query.SpecieId);

        return specieDto;
    }
}