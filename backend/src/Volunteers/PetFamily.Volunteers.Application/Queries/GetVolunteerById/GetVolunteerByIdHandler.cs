using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.Volunteers.Application.Commands.AddPet;
using PetFamily.Volunteers.Contracts.Dtos.Volunteer;

namespace PetFamily.Volunteers.Application.Queries.GetVolunteerById;
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
