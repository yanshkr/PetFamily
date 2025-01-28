using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Volunteers.Entities;
using PetFamily.Domain.Volunteers.Ids;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Domain.Volunteers;
public class Volunteer : BaseEntity<VolunteerId>
{
    private readonly List<Pet> _pets = [];
    private readonly List<PaymentInfo> _paymentInfos = [];
    private readonly List<SocialMedia> _socialMedias = [];

#pragma warning disable CS8618
    private Volunteer() { }
#pragma warning restore CS8618

    private Volunteer(
        VolunteerId id,
        FullName fullName,
        Email email,
        Description description,
        ExperienceYears experienceYears,
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
    public Description Description { get; private set; }
    public ExperienceYears ExperienceYears { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }

    public IReadOnlyList<Pet> Pets => _pets;
    public IReadOnlyList<PaymentInfo> PaymentInfos => _paymentInfos;
    public IReadOnlyList<SocialMedia> SocialMedias => _socialMedias;

    public static Result<Volunteer, Error> Create(
        VolunteerId id,
        FullName fullName,
        Email email,
        Description description,
        ExperienceYears experienceYears,
        PhoneNumber phoneNumber
        )
    {
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
