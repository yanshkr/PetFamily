using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Entities;
public class Volunteer : BaseEntity<Guid>
{
    private readonly List<Pet> _pets = [];
    private readonly List<PaymentInfo> _paymentInfos = [];
    private readonly List<SocialMedia> _socialMedias = [];

    private Volunteer(
        string name,
        string surname,
        string patronymic,
        string email,
        string description,
        uint experienceYears,
        PhoneNumber phoneNumber
        ) : base()
    {
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
        Email = email;
        Description = description;
        ExperienceYears = experienceYears;
        PhoneNumber = phoneNumber;
    }

    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Patronymic { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; }
    public uint ExperienceYears { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }

    public IReadOnlyCollection<Pet> Pets => _pets;
    public IReadOnlyCollection<PaymentInfo> PaymentInfos => _paymentInfos;
    public IReadOnlyCollection<SocialMedia> SocialMedias => _socialMedias;

    public static Result<Volunteer, string> Create(
        string name,
        string surname,
        string patronymic,
        string email,
        string description,
        uint experienceYears,
        PhoneNumber phoneNumber
        )
    {
        if (string.IsNullOrWhiteSpace(name))
            return "Name should not be empty";

        if (string.IsNullOrWhiteSpace(surname))
            return "Surname should not be empty";

        if (string.IsNullOrWhiteSpace(patronymic))
            return "Patronymic should not be empty";

        if (string.IsNullOrWhiteSpace(email))
            return "Email should not be empty";

        if (string.IsNullOrWhiteSpace(description))
            return "Description should not be empty";

        return new Volunteer(
            name,
            surname,
            patronymic,
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
