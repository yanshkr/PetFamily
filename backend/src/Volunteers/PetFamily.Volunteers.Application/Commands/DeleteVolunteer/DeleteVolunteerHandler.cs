using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Database;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.SharedKernel.Structs;
using PetFamily.Volunteers.Domain.Ids;

namespace PetFamily.Volunteers.Application.Commands.DeleteVolunteer;
public class DeleteVolunteerHandler
    : ICommandHandler<VolunteerId, DeleteVolunteerCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    public DeleteVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        [FromKeyedServices(UnitOfWorkSelector.Volunteers)] IUnitOfWork unitOfWork,
        ILogger<DeleteVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<VolunteerId, ErrorList>> HandleAsync(
        DeleteVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var volunteerResult = await _volunteersRepository.GetByIdAsync(command.Id, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        if (volunteerResult.Value.IsDeleted && command.IsSoftDelete)
            return volunteerResult.Value.Id;

        if (command.IsSoftDelete)
            volunteerResult.Value.Delete();
        else
            _volunteersRepository.Delete(volunteerResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return volunteerResult.Value.Id;
    }
}
