namespace PetFamily.Application.Dtos;
public record VolunteerDto(
    Guid Id,
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Description,
    int ExperienceYears,
    IReadOnlyList<PaymentInfoDto> PaymentInfos,
    IReadOnlyList<SocialMediaDto> SocialMedias);