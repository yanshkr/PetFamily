using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Commands.UpdateVolunteerMainInfo;
public record UpdateVolunteerCommand(
    Guid Id,
    string FirstName,
    string MiddleName,
    string Surname,
    string Email,
    string Description,
    int Experience,
    string PhoneNumber) : ICommand;
