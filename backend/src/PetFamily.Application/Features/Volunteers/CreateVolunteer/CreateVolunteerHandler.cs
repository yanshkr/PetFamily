using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;
using PetFamily.Domain.Volunteers.Ids;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.CreateVolunteer;
public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateVolunteerCommand> _validator;
    private readonly ILogger<CreateVolunteerHandler> _logger;
    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateVolunteerCommand> validator,
        ILogger<CreateVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<VolunteerId, ErrorList>> HandleAsync(
        CreateVolunteerCommand request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var fullName = FullName.Create(request.FullName.FirstName, request.FullName.MiddleName, request.FullName.Surname).Value;
        var email = Email.Create(request.Email).Value;
        var description = Description.Create(request.Description).Value;
        var experience = ExperienceYears.Create(request.Experience).Value;
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber).Value;

        var volunteer = Volunteer.Create(VolunteerId.NewVolunteerId(), fullName, email, description, experience, phoneNumber);

        if (volunteer.IsFailure)
            return volunteer.Error.ToErrorList();

        var guidVolunteer = await _volunteersRepository.AddAsync(volunteer.Value, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return guidVolunteer;
    }
}