namespace PetFamily.Application.Dtos;
public class VolunteerDto
{
    public Guid Id { get; init; }
    public string FirstName { get; init; } = null!;
    public string MiddleName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
    public string Description { get; init; } = null!;
    public int ExperienceYears { get; init; }
    public IReadOnlyList<PaymentInfoDto> PaymentInfos { get; init; } =[];
    public IReadOnlyList<SocialMediaDto> SocialMedias { get; init; } = [];
}