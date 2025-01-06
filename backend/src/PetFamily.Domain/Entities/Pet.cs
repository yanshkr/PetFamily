using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Entities;
public class Pet : BaseEntity<Guid>
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

    public Result UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure("Name cannot be empty");

        Name = name;

        return Result.Success();
    }
    public Result UpdateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure("Description cannot be empty");

        Description = description;

        return Result.Success();
    }
    public Result UpdateType(PetType type)
    {
        if (type == PetType.Undefined)
            return Result.Failure("PetType cannot be undefined");

        Type = type;

        return Result.Success();
    }
    public Result UpdateBreed(PetBreed breed)
    {
        if (breed == null)
            return Result.Failure("Breed cannot be null");

        Breed = breed;

        return Result.Success();
    }
    public Result UpdateSpecie(PetSpecie specie)
    {
        if (specie == null)
            return Result.Failure("Specie cannot be null");

        Specie = specie;

        return Result.Success();
    }
    public Result UpdateColor(string color)
    {
        if (string.IsNullOrWhiteSpace(color))
            return Result.Failure("Color cannot be empty");

        Color = color;

        return Result.Success();
    }
    public Result UpdateHealthInfo(string healthInfo)
    {
        if (string.IsNullOrWhiteSpace(healthInfo))
            return Result.Failure("HealthInfo cannot be empty");

        HealthInfo = healthInfo;

        return Result.Success();
    }
    public Result UpdateAddress(Address address)
    {
        Address = address;

        return Result.Success();
    }
    public Result UpdatePhoneNumber(PhoneNumber phoneNumber)
    {
        PhoneNumber = phoneNumber;

        return Result.Success();
    }
    public Result UpdateWeight(WeightMeasurement weight)
    {
        Weight = weight;

        return Result.Success();
    }
    public Result UpdateHeight(HeightMeasurement height)
    {
        Height = height;

        return Result.Success();
    }
    public Result UpdateBirthDate(DateTime birthDate)
    {
        if (birthDate == default)
            return Result.Failure("BirthDate cannot be empty");

        BirthDate = birthDate;

        return Result.Success();
    }
    public Result UpdateIsSterilized(bool isSterilized)
    {
        IsSterilized = isSterilized;

        return Result.Success();
    }
    public Result UpdateStatus(PetStatus status)
    {
        if (status == PetStatus.Undefined)
            return Result.Failure("PetStatus cannot be undefined");

        Status = status;

        return Result.Success();
    }

    public Result AddVaccine(Vaccine vaccine)
    {
        _vaccines.Add(vaccine);

        return Result.Success();
    }
    public Result DeleteVaccine(Vaccine vaccine)
    {
        if (!_vaccines.Contains(vaccine))
            return Result.Failure("Vaccine not found");

        _vaccines.Remove(vaccine);

        return Result.Success();
    }
    public Result AddPaymentInfo(PaymentInfo paymentInfo)
    {
        _paymentInfos.Add(paymentInfo);

        return Result.Success();
    }
    public Result DeletePaymentInfo(PaymentInfo paymentInfo)
    {
        _paymentInfos.Remove(paymentInfo);

        return Result.Success();
    }
}
