namespace PetFamily.Application.Features.Volunteers.CreateVolunteer;
public record CreateVolunteerRequest(
    string FirstName,
    string MiddleName, 
    string Surname, 
    string Email, 
    string Description, 
    int Experience,
    string PhoneNumber);