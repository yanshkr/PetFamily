using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Volunteers.Entities;
using PetFamily.Domain.Volunteers.Ids;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Domain.Volunteers;
public class Volunteer : SoftDeletableEntity<VolunteerId>
{
    private readonly List<Pet> _pets = [];
    private List<PaymentInfo> _paymentInfos = [];
    private List<SocialMedia> _socialMedias = [];

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
        PhoneNumber phoneNumber)
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

    public void UpdateMainInfo(
        FullName fullName,
        Email email,
        Description description,
        ExperienceYears experienceYears,
        PhoneNumber phoneNumber)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        ExperienceYears = experienceYears;
        PhoneNumber = phoneNumber;
    }
    public void UpdateSocialMedia(IEnumerable<SocialMedia> socialMedias)
    {
        _socialMedias = socialMedias.ToList();
    }
    public void UpdatePaymentInfo(IEnumerable<PaymentInfo> paymentInfos)
    {
        _paymentInfos = paymentInfos.ToList();
    }

    public Result<Pet, Error> GetPetById(PetId petId)
    {
        var pet = _pets.FirstOrDefault(p => p.Id == petId);
        if (pet == null)
            return Errors.General.NotFound(petId);

        return pet;
    }
    public UnitResult<Error> AddPet(Pet pet)
    {
        if (_pets.Contains(pet))
            return Errors.General.AlreadyExists(pet.Id);

        var petPosition = PetPosition.Create(_pets.Count + 1);
        if (petPosition.IsFailure)
            return petPosition.Error;

        pet.SetPetPosition(petPosition.Value);

        _pets.Add(pet);

        return UnitResult.Success<Error>();
    }
    public UnitResult<Error> RemovePet(Pet pet)
    {
        if (!_pets.Contains(pet))
            return Errors.General.NotFound(pet.Id);

        _pets.Remove(pet);

        foreach (var leastPet in _pets.Where(p => p.PetPosition > pet.PetPosition))
        {
            var newPetPosition = PetPosition.Create(leastPet.PetPosition.Value - 1);
            if (newPetPosition.IsFailure)
                return newPetPosition.Error;

            leastPet.SetPetPosition(newPetPosition.Value);
        }

        return UnitResult.Success<Error>();
    }
    public UnitResult<Error> MovePet(Pet pet, PetPosition petPosition)
    {
        if (!_pets.Contains(pet))
            return Errors.General.NotFound(pet.Id);

        if (_pets.Count < petPosition.Value)
            return Errors.General.ValueIsInvalid("PetPosition");

        int direction = pet.PetPosition > petPosition ? 1 :
                        pet.PetPosition < petPosition ? -1 : 0;

        if (direction == 0)
            return UnitResult.Success<Error>();

        foreach (var petToMove in _pets.Where(p => p != pet))
        {
            if ((direction > 0 && petToMove.PetPosition >= petPosition && petToMove.PetPosition < pet.PetPosition) ||
                (direction < 0 && petToMove.PetPosition <= petPosition && petToMove.PetPosition > pet.PetPosition))
            {
                var newPetPosition = PetPosition.Create(petToMove.PetPosition.Value + direction);
                if (newPetPosition.IsFailure)
                    return newPetPosition.Error;

                petToMove.SetPetPosition(newPetPosition.Value);
            }
        }

        pet.SetPetPosition(petPosition);

        return UnitResult.Success<Error>();
    }
    public UnitResult<Error> MovePetToStart(Pet pet)
    {
        return MovePet(pet, PetPosition.Start);
    }
    public UnitResult<Error> MovePetToEnd(Pet pet)
    {
        var newPetPosition = PetPosition.Create(_pets.Count);
        if (newPetPosition.IsFailure)
            return newPetPosition.Error;

        return MovePet(pet, newPetPosition.Value);
    }

    public new void Delete()
    {
        base.Delete();

        foreach (var pet in _pets)
        {
            pet.Delete();
        }
    }
    public new void Restore()
    {
        base.Restore();

        foreach (var pet in _pets)
        {
            pet.Restore();
        }
    }
}
