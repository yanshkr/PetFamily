﻿using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.Entities;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Domain.Enums;
using PetFamily.Volunteers.Domain.Ids;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Domain.Entities;
public class Pet : SoftDeletableEntity<PetId>
{
    private readonly List<Vaccine> _vaccines = [];
    private readonly List<PaymentInfo> _paymentInfos = [];
    private readonly List<Photo> _photos = [];

    public Volunteer Volunteer { get; private set; } = null!;

#pragma warning disable CS8618
    private Pet() { }
#pragma warning restore CS8618

    private Pet(
        PetId id,
        Name name,
        Description description,
        PetType type,
        PetClassification petClassification,
        Color color,
        HealthInfo healthInfo,
        Address address,
        PhoneNumber phoneNumber,
        WeightMeasurement weight,
        HeightMeasurement height,
        DateTime birthDate,
        bool isSterilized,
        PetStatus status) : base()
    {
        Id = id;
        Name = name;
        Description = description;
        Type = type;
        PetClassification = petClassification;
        Color = color;
        HealthInfo = healthInfo;
        Address = address;
        PhoneNumber = phoneNumber;
        Weight = weight;
        Height = height;
        BirthDate = birthDate;
        IsSterilized = isSterilized;
        Status = status;
        MainPhoto = Photo.CreateEmpty();
    }

    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public PetType Type { get; private set; }
    public PetClassification PetClassification { get; private set; }
    public Color Color { get; private set; }
    public HealthInfo HealthInfo { get; private set; }
    public Address Address { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public WeightMeasurement Weight { get; private set; }
    public HeightMeasurement Height { get; private set; }
    public DateTime BirthDate { get; private set; }
    public bool IsSterilized { get; private set; }
    public bool IsVaccinated => _vaccines.Count != 0;

    public PetPosition PetPosition { get; private set; } = null!;
    public Photo MainPhoto { get; private set; }

    public IReadOnlyList<Vaccine> Vaccines => _vaccines;
    public IReadOnlyList<PaymentInfo> PaymentInfos => _paymentInfos;
    public IReadOnlyList<Photo> Photos => _photos;

    public PetStatus Status { get; private set; }

    public static Result<Pet, Error> Create(
        PetId id,
        Name name,
        Description description,
        PetType type,
        PetClassification petClassification,
        Color color,
        HealthInfo healthInfo,
        Address address,
        PhoneNumber phoneNumber,
        WeightMeasurement weight,
        HeightMeasurement height,
        DateTime birthDate,
        bool isSterilized,
        PetStatus status)
    {
        if (type == PetType.Undefined)
            return Errors.General.ValueIsInvalid("Type");

        if (birthDate == default)
            return Errors.General.ValueIsInvalid("BirthDate");

        if (status == PetStatus.Undefined)
            return Errors.General.ValueIsInvalid("Status");

        return new Pet(
            id,
            name,
            description,
            type,
            petClassification,
            color,
            healthInfo,
            address,
            phoneNumber,
            weight,
            height,
            birthDate,
            isSterilized,
            status);
    }

    public UnitResult<Error> UpdateMainPhoto(Photo photo)
    {
        if (!_photos.Contains(photo))
            return Errors.General.ValueIsRequired("Photo");

        MainPhoto = photo;

        return UnitResult.Success<Error>();
    }
    public void AddPhotos(List<Photo> photos)
    {
        if (photos.Count == 0)
            return;

        MainPhoto ??= photos.First();

        _photos.AddRange(photos);
    }
    public void RemovePhotos(List<Photo> photos)
    {
        foreach (var photo in photos)
        {
            _photos.Remove(photo);
        }
    }

    public void UpdateMainInfo(
        Name name,
        Description description,
        PetType type,
        PetClassification petClassification,
        Color color,
        HealthInfo healthInfo,
        Address address,
        PhoneNumber phoneNumber,
        WeightMeasurement weight,
        HeightMeasurement height,
        DateTime birthDate,
        bool isSterilized)
    {
        Name = name;
        Description = description;
        Type = type;
        PetClassification = petClassification;
        Color = color;
        HealthInfo = healthInfo;
        Address = address;
        PhoneNumber = phoneNumber;
        Weight = weight;
        Height = height;
        BirthDate = birthDate;
        IsSterilized = isSterilized;
    }
    public void UpdateStatus(PetStatus status)
    {
        Status = status;
    }

    public void SetPetPosition(PetPosition serialNumber)
    {
        PetPosition = serialNumber;
    }
}