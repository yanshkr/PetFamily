using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Ids;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Entities;
public class Volunteer : BaseEntity<VolunteerId>
{
    public const int MAX_DESCRIPTION_LENGTH = 500;

    public const int MIN_EXPERIENCE_YEARS = 0;
    public const int MAX_EXPERIENCE_YEARS = 100;

    private readonly List<Pet> _pets = [];
    private readonly List<PaymentInfo> _paymentInfos = [];
    private readonly List<SocialMedia> _socialMedias = [];

    private Volunteer() { }

    private Volunteer(
        VolunteerId id,
        FullName fullName,
        Email email,
        string description,
        int experienceYears,
        PhoneNumber phoneNumber
        ) : base()
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Description = description;
        ExperienceYears = experienceYears;
        PhoneNumber = phoneNumber;
    }

    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public string Description { get; private set; }
    public int ExperienceYears { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }

    public IReadOnlyList<Pet> Pets => _pets;
    public IReadOnlyList<PaymentInfo> PaymentInfos => _paymentInfos;
    public IReadOnlyList<SocialMedia> SocialMedias => _socialMedias;

    public static Result<Volunteer, Error> Create(
        VolunteerId id,
        FullName fullName,
        Email email,
        string description,
        int experienceYears,
        PhoneNumber phoneNumber
        )
    {

        if (string.IsNullOrWhiteSpace(description) || description.Length > MAX_DESCRIPTION_LENGTH)
            return Errors.General.ValueIsInvalid("Description");

        if (experienceYears is < MIN_EXPERIENCE_YEARS or > MAX_EXPERIENCE_YEARS)
            return Errors.General.ValueIsInvalid("ExperienceYears");

        return new Volunteer(
            id,
            fullName,
            email,
            description,
            experienceYears,
            phoneNumber
            );
    }

    public int GetHelpPetsCount() => _pets.Count(p => p.Status == PetStatus.NeedHelp);
    public int GetShelterLookingCount() => _pets.Count(p => p.Status == PetStatus.ShelterLooking);
    public int GetShelterFoundCount() => _pets.Count(p => p.Status == PetStatus.ShelterFound);
}
