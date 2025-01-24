using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Ids;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Entities;
public class Pet : BaseEntity<PetId>
{
    public const int MAX_NAME_LENGTH = 100;
    public const int MAX_DESCRIPTION_LENGTH = 500;

    public const int MAX_COLOR_LENGTH = 50;
    public const int MAX_HEALTH_INFO_LENGTH = 500;

    private readonly List<PaymentInfo> _paymentInfos = [];
    private readonly List<Vaccine> _vaccines = [];

    public Volunteer Volunteer { get; private set; } = null!;

    private Pet() { }

    private Pet(
        PetId id,
        string name,
        string description,
        PetType type,
        PetBreed breed,
        PetSpecie specie,
        string color,
        string healthInfo,
        Address address,
        PhoneNumber phoneNumber,
        WeightMeasurement weight,
        HeightMeasurement height,
        DateTime birthDate,
        bool isSterilized,
        PetStatus status
        ) : base()
    {
        Id = id;
        Name = name;
        Description = description;
        Type = type;
        Breed = breed;
        Specie = specie;
        Color = color;
        HealthInfo = healthInfo;
        Address = address;
        PhoneNumber = phoneNumber;
        Weight = weight;
        Height = height;
        BirthDate = birthDate;
        IsSterilized = isSterilized;
        Status = status;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public PetType Type { get; private set; }
    public PetBreed Breed { get; private set; }
    public PetSpecie Specie { get; private set; }
    public string Color { get; private set; }
    public string HealthInfo { get; private set; }
    public Address Address { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public WeightMeasurement Weight { get; private set; }
    public HeightMeasurement Height { get; private set; }
    public DateTime BirthDate { get; private set; }
    public bool IsSterilized { get; private set; }
    public bool IsVaccinated => _vaccines.Count != 0;

    public IReadOnlyList<Vaccine> Vaccines => _vaccines;
    public IReadOnlyList<PaymentInfo> PaymentInfos => _paymentInfos;

    public PetStatus Status { get; private set; }

    public static Result<Pet, Error> Create(
        PetId id,
        string name,
        string description,
        PetType type,
        PetBreed breed,
        PetSpecie specie,
        string color,
        string healthInfo,
        Address address,
        PhoneNumber phoneNumber,
        WeightMeasurement weight,
        HeightMeasurement height,
        DateTime birthDate,
        bool isSterilized,
        PetStatus status
        )
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH)
            return Errors.General.ValueIsInvalid("Name");

        if (string.IsNullOrWhiteSpace(description) || description.Length > MAX_DESCRIPTION_LENGTH)
            return Errors.General.ValueIsInvalid("Description");

        if (type == PetType.Undefined)
            return Errors.General.ValueIsInvalid("Type");

        if (string.IsNullOrWhiteSpace(color) || color.Length > MAX_COLOR_LENGTH)
            return Errors.General.ValueIsInvalid("Color");

        if (string.IsNullOrWhiteSpace(healthInfo) || healthInfo.Length > MAX_HEALTH_INFO_LENGTH)
            return Errors.General.ValueIsInvalid("HealthInfo");

        if (birthDate == default)
            return Errors.General.ValueIsInvalid("BirthDate");

        if (status == PetStatus.Undefined)
            return Errors.General.ValueIsInvalid("Status");

        return new Pet(
            id,
            name,
            description,
            type,
            breed,
            specie,
            color,
            healthInfo,
            address,
            phoneNumber,
            weight,
            height,
            birthDate,
            isSterilized,
            status
            );
    }
}