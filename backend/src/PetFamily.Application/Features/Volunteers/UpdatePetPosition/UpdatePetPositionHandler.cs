using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Features.Volunteers.UpdateVolunteerMainInfo;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers.Ids;
using PetFamily.Domain.Volunteers.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Features.Volunteers.UpdatePetPosition;
public class UpdatePetPositionHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdatePetPositionCommand> _validator;
    private readonly ILogger<UpdatePetPositionHandler> _logger;
    public UpdatePetPositionHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
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

        var volunteer = await _volunteersRepository.GetByIdAsync(command.VolunteerId, cancellationToken);
        if (volunteer.IsFailure)
            return volunteer.Error.ToErrorList();

        var pet = volunteer.Value.GetPetById(command.PetId);
        if (pet.IsFailure)
            return pet.Error.ToErrorList();

        var position = PetPosition.Create(command.Position).Value;
        var moveResult = volunteer.Value.MovePet(pet.Value, position);

        if (moveResult.IsFailure)
            return moveResult.Error.ToErrorList();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return volunteer.Value.Id;
    }
}
