using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Features.Commands.Volunteers.AddPet;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Features.Queries.GetVolunteerById;
public class GetVolunteerByIdHandler 
    : IQueryHandler<Result<VolunteerDto, ErrorList>, GetVolunteerByIdQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<AddPetHandler> _logger;
    public GetVolunteerByIdHandler(
        IReadDbContext readDbContext,
        ILogger<AddPetHandler> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<Result<VolunteerDto, ErrorList>> HandleAsync(
        GetVolunteerByIdQuery query,
        CancellationToken cancellationToken = default)
    {

        var volunteer = await _readDbContext.Volunteers
            .FirstOrDefaultAsync(v => v.Id == query.Id, cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(query.Id).ToErrorList();

        return volunteer;
    }
}
