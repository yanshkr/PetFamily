using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Database;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.SharedKernel.Structs;
using PetFamily.Volunteers.Domain.Enums;
using PetFamily.Volunteers.Domain.Ids;

namespace PetFamily.Volunteers.Application.Commands.UpdatePetStatus;
public class UpdatePetStatusHandler
    : ICommandHandler<PetId, UpdatePetStatusCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdatePetStatusHandler> _logger;
    public UpdatePetStatusHandler(
        IVolunteersRepository volunteersRepository,
        [FromKeyedServices(UnitOfWorkSelector.Volunteers)] IUnitOfWork unitOfWork,
        ILogger<UpdatePetStatusHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<PetId, ErrorList>> HandleAsync(
        UpdatePetStatusCommand command,
        CancellationToken cancellationToken = default)
    {
        var volunteerResult = await _volunteersRepository.GetByIdAsync(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petResult = volunteerResult.Value.GetPetById(command.PetId);
        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();

        if (command.Status == PetStatus.Undefined)
            return Errors.General.ValueIsInvalid("pet.status").ToErrorList();

        if (petResult.Value.Status == command.Status)
            return petResult.Value.Id;

        petResult.Value.UpdateStatus(command.Status);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return petResult.Value.Id;
    }
}
