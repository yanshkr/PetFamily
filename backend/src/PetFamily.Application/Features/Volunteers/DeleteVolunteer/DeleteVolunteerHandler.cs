using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Features.Volunteers.DeleteVolunteer;
public class DeleteVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    public DeleteVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<DeleteVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> HandleAsync(
        DeleteVolunteerRequest request,
        CancellationToken cancellationToken = default
        )
    {
        _logger.LogDebug("DeleteVolunteerRequest: {@request}", request);

        var volunteer = await _volunteersRepository.GetByIdAsync(request.Id, cancellationToken);
        if (volunteer.IsFailure)
            return volunteer.Error;

        if (volunteer.Value.IsDeleted && request.IsSoftDelete)
        {
            _logger.LogWarning("Volunteer already deleted: {@id}", volunteer.Value.Id.Value);
            return volunteer.Value.Id.Value;
        }

        if (request.IsSoftDelete)
            volunteer.Value.Delete();
        else
            _volunteersRepository.Delete(volunteer.Value);

        await _volunteersRepository.SaveAsync(cancellationToken);

        _logger.LogDebug("Volunteer deleted: {@id}", volunteer.Value.Id.Value);

        return volunteer.Value.Id.Value;
    }
}
