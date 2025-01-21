using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Ids;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Entities;
public class Volunteer : BaseEntity<VolunteerId>
{
    private readonly List<Pet> _pets = [];
    private readonly List<PaymentInfo> _paymentInfos = [];
    private readonly List<SocialMedia> _socialMedias = [];

    private Volunteer() { }

    private Volunteer(
        VolunteerId id,
        FullName fullName,
        string email,
        string description,
        uint experienceYears,
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
    public string Email { get; private set; }
    public string Description { get; private set; }
    public uint ExperienceYears { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }

    public IReadOnlyList<Pet> Pets => _pets;
    public IReadOnlyList<PaymentInfo> PaymentInfos => _paymentInfos;
    public IReadOnlyList<SocialMedia> SocialMedias => _socialMedias;

    public static Result<Volunteer, string> Create(
        VolunteerId id,
        FullName fullName,
        string email,
        string description,
        uint experienceYears,
        PhoneNumber phoneNumber
        )
    {

        if (string.IsNullOrWhiteSpace(email))
            return "Email should not be empty";

        if (string.IsNullOrWhiteSpace(description))
            return "Description should not be empty";

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
