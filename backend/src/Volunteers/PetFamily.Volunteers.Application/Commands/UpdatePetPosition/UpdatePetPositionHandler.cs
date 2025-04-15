using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Database;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.SharedKernel.Structs;
using PetFamily.Volunteers.Domain.Ids;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.UpdatePetPosition;
public class UpdatePetPositionHandler
        : ICommandHandler<VolunteerId, UpdatePetPositionCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdatePetPositionCommand> _validator;
    private readonly ILogger<UpdatePetPositionHandler> _logger;
    public UpdatePetPositionHandler(
        IVolunteersRepository volunteersRepository,
        [FromKeyedServices(UnitOfWorkSelector.Volunteers)] IUnitOfWork unitOfWork,
        IValidator<UpdatePetPositionCommand> validator,
        ILogger<UpdatePetPositionHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<VolunteerId, ErrorList>> HandleAsync(
        UpdatePetPositionCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var volunteerResult = await _volunteersRepository.GetByIdAsync(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petResult = volunteerResult.Value.GetPetById(command.PetId);
        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();

        var position = PetPosition.Create(command.Position).Value;
        var moveResult = volunteerResult.Value.MovePet(petResult.Value, position);

        if (moveResult.IsFailure)
            return moveResult.Error.ToErrorList();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return volunteerResult.Value.Id;
    }
}
