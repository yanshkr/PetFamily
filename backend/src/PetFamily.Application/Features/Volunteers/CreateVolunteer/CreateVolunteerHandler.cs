using CSharpFunctionalExtensions;
using PetFamily.Domain.Entities;
using PetFamily.Domain.Ids;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.CreateVolunteer;
public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    public CreateVolunteerHandler(IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid, Error>> HandleAsync(CreateVolunteerRequest request, CancellationToken cancellationToken = default)
    {
        var fullName = FullName.Create(
            request.FirstName, 
            request.MiddleName, 
            request.Surname);

        if (fullName.IsFailure)
            return fullName.Error;

        var email = Email.Create(request.Email);
        if (email.IsFailure)
            return email.Error;

        var phoneNumber = PhoneNumber.Create(request.PhoneNumber);
        if (phoneNumber.IsFailure)
            return phoneNumber.Error;

        var volunteer = Volunteer.Create(
            VolunteerId.NewVolunteerId(), 
            fullName.Value, 
            email.Value, 
            request.Description, 
            request.Experience, 
            phoneNumber.Value);

        if (volunteer.IsFailure)
            return volunteer.Error;

        var guidVolunteer = await _volunteersRepository.AddAsync(volunteer.Value, cancellationToken);

        return guidVolunteer;
    }
}