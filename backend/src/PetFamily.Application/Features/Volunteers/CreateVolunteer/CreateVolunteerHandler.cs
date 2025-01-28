using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventSource;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Volunteers;
using PetFamily.Domain.Volunteers.Ids;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.CreateVolunteer;
public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<CreateVolunteerHandler> _logger;
    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<CreateVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> HandleAsync(
        CreateVolunteerRequest request,
        CancellationToken cancellationToken = default
        )
    {
        _logger.LogDebug("CreateVolunteerRequest: {@request}", request);

        var fullName = FullName.Create(request.FirstName, request.MiddleName, request.Surname).Value;
        var email = Email.Create(request.Email).Value;
        var description = Description.Create(request.Description).Value;
        var experience = ExperienceYears.Create(request.Experience).Value;
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber).Value;

        var volunteer = Volunteer.Create(VolunteerId.NewVolunteerId(), fullName, email, description, experience, phoneNumber);

        if (volunteer.IsFailure)
            return volunteer.Error;

        var guidVolunteer = await _volunteersRepository.AddAsync(volunteer.Value, cancellationToken);

        _logger.LogDebug("Volunteer created: {@volunteer}", volunteer.Value);

        return guidVolunteer;
    }
}