using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.Volunteers.Contracts.Dtos.Pet;

namespace PetFamily.Volunteers.Application.Queries.GetPetById;
public class GetPetByIdHandler
    : IQueryHandler<Result<PetDto, ErrorList>, GetPetByIdQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<GetPetByIdHandler> _logger;
    public GetPetByIdHandler(
        IReadDbContext readDbContext,
        ILogger<GetPetByIdHandler> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<Result<PetDto, ErrorList>> HandleAsync(
        GetPetByIdQuery query,
        CancellationToken cancellationToken = default)
    {

        var pet = await _readDbContext.Pets
            .FirstOrDefaultAsync(v => v.Id == query.Id, cancellationToken);

        if (pet is null)
            return Errors.General.NotFound(query.Id).ToErrorList();

        return pet;
    }
}
