using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Features.Volunteers.UpdateVolunteerMainInfo.Contracts;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.UpdateVolunteer;
public class UpdateVolunteerMainInfoHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<UpdateVolunteerMainInfoHandler> _logger;
    public UpdateVolunteerMainInfoHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdateVolunteerMainInfoHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> HandleAsync(
        UpdateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("UpdateVolunteerRequest: {@request}", request);

        var volunteer = await _volunteersRepository.GetByIdAsync(request.Id, cancellationToken);
        if (volunteer.IsFailure)
            return volunteer.Error;

        var fullName = FullName.Create(request.Dto.FirstName, request.Dto.MiddleName, request.Dto.Surname).Value;
        var email = Email.Create(request.Dto.Email).Value;
        var description = Description.Create(request.Dto.Description).Value;
        var experience = ExperienceYears.Create(request.Dto.Experience).Value;
        var phoneNumber = PhoneNumber.Create(request.Dto.PhoneNumber).Value;

        volunteer.Value.UpdateMainInfo(fullName, email, description, experience, phoneNumber);

        await _volunteersRepository.SaveAsync(cancellationToken);

        _logger.LogDebug("Volunteer updated: {@id}", volunteer.Value.Id.Value);

        return volunteer.Value.Id.Value;
    }
}
