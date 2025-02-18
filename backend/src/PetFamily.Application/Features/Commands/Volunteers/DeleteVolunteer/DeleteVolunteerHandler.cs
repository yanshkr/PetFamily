using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.Features.Commands.Volunteers.DeleteVolunteer;
public class DeleteVolunteerHandler
    : ICommandHandler<VolunteerId, DeleteVolunteerCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    public DeleteVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        ILogger<DeleteVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<VolunteerId, ErrorList>> HandleAsync(
        DeleteVolunteerCommand request,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _volunteersRepository.GetByIdAsync(request.Id, cancellationToken);
        if (volunteer.IsFailure)
            return volunteer.Error.ToErrorList();

        if (volunteer.Value.IsDeleted && request.IsSoftDelete)
            return volunteer.Value.Id;

        if (request.IsSoftDelete)
            volunteer.Value.Delete();
        else
            _volunteersRepository.Delete(volunteer.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return volunteer.Value.Id;
    }
}
