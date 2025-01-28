using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Volunteers;
using PetFamily.Domain.Volunteers.Ids;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.CreateVolunteer;
public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    public CreateVolunteerHandler(IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid, Error>> HandleAsync(
        CreateVolunteerRequest request,
        CancellationToken cancellationToken = default
        )
    {
        var fullName = FullName.Create(request.FirstName, request.MiddleName, request.Surname).Value;
        var email = Email.Create(request.Email).Value;
        var description = Description.Create(request.Description).Value;
        var experience = ExperienceYears.Create(request.Experience).Value;
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber).Value;

        var volunteer = Volunteer.Create(VolunteerId.NewVolunteerId(), fullName, email, description, experience, phoneNumber);

        if (volunteer.IsFailure)
            return volunteer.Error;

        var guidVolunteer = await _volunteersRepository.AddAsync(volunteer.Value, cancellationToken);

        return guidVolunteer;
    }
}