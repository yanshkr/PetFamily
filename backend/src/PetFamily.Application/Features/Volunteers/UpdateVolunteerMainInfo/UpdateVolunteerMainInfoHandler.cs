using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers.Ids;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.UpdateVolunteerMainInfo;
public class UpdateVolunteerMainInfoHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateVolunteerCommand> _validator;
    private readonly ILogger<UpdateVolunteerMainInfoHandler> _logger;
    public UpdateVolunteerMainInfoHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        IValidator<UpdateVolunteerCommand> validator,
        ILogger<UpdateVolunteerMainInfoHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<VolunteerId, ErrorList>> HandleAsync(
        UpdateVolunteerCommand request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var volunteer = await _volunteersRepository.GetByIdAsync(request.Id, cancellationToken);
        if (volunteer.IsFailure)
            return volunteer.Error.ToErrorList();

        var fullName = FullName.Create(request.FirstName, request.MiddleName, request.Surname).Value;
        var email = Email.Create(request.Email).Value;
        var description = Description.Create(request.Description).Value;
        var experience = ExperienceYears.Create(request.Experience).Value;
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber).Value;

        volunteer.Value.UpdateMainInfo(fullName, email, description, experience, phoneNumber);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return volunteer.Value.Id;
    }
}
