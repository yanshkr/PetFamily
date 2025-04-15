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

namespace PetFamily.Volunteers.Application.Commands.UpdatePetMainPhoto;
public class UpdatePetMainPhotoHandler
    : ICommandHandler<PetId, UpdatePetMainPhotoCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdatePetMainPhotoCommand> _validator;
    private readonly ILogger<UpdatePetMainPhotoHandler> _logger;
    public UpdatePetMainPhotoHandler(
        IVolunteersRepository volunteersRepository,
        [FromKeyedServices(UnitOfWorkSelector.Volunteers)] IUnitOfWork unitOfWork,
        IValidator<UpdatePetMainPhotoCommand> validator,
        ILogger<UpdatePetMainPhotoHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<PetId, ErrorList>> HandleAsync(
        UpdatePetMainPhotoCommand command,
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

        var photo = Photo.Create(command.FileName).Value;

        var updateResult = petResult.Value.UpdateMainPhoto(photo);
        if (updateResult.IsFailure)
            return updateResult.Error.ToErrorList();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return petResult.Value.Id;
    }
}