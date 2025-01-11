using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Ids;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Entities;
public class Pet : BaseEntity<PetId>
{
    private readonly List<Vaccine> _vaccines = [];
    private readonly List<PaymentInfo> _paymentInfos = [];

    private Pet(
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


    public static Result<Pet, string> Create(
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
        if (string.IsNullOrWhiteSpace(name))
            return "Name cannot be empty";

        if (string.IsNullOrWhiteSpace(description))
            return "Description cannot be empty";

        if (type == PetType.Undefined)
            return "PetType cannot be undefined";

        if (string.IsNullOrWhiteSpace(color))
            return "Color cannot be empty";

        if (string.IsNullOrWhiteSpace(healthInfo))
            return "HealthInfo cannot be empty";

        if (birthDate == default)
            return "BirthDate cannot be empty";

        if (status == PetStatus.Undefined)
            return "PetStatus cannot be undefined";

        return new Pet(
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

public record VaccinesInfo
{

}