using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Species;
using PetFamily.Domain.Species.Entities;
using PetFamily.Domain.Species.Ids;
using PetFamily.Domain.Volunteers;
using PetFamily.Domain.Volunteers.Entities;
using PetFamily.Domain.Volunteers.Enums;
using PetFamily.Domain.Volunteers.Ids;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Domain.UnitTests;

public class VolunteerTests
{
    [Fact]
    public void Create_Empty_Volunteer_Add_Pet_Pet_Should_Set_First_Serial()
    {
        var volunteer = CreateVolunteerWithPets(0);
        var pet = CreatePet();

        var result = volunteer.AddPet(pet);

        var addedPetResult = volunteer.GetPetById(pet.Id);

        Assert.True(result.IsSuccess);
        Assert.True(addedPetResult.IsSuccess);
        Assert.Equal(pet.Id, addedPetResult.Value.Id);
        Assert.Equal(1, addedPetResult.Value.PetPosition.Value);
    }
    [Fact]
    public void Create_Volunteer_With_Three_Pet_Add_Pet_Should_Set_Fourth_Serial()
    {
        var volunteer = CreateVolunteerWithPets(3);
        var pet = CreatePet();

        var result = volunteer.AddPet(pet);

        var addedPetResult = volunteer.GetPetById(pet.Id);

        Assert.True(result.IsSuccess);
        Assert.True(addedPetResult.IsSuccess);
        Assert.Equal(pet.Id, addedPetResult.Value.Id);
        Assert.Equal(4, addedPetResult.Value.PetPosition.Value);
    }
    [Fact]
    public void Create_Volunteer_With_Three_Pet_Add_Pet_With_Same_Id_Should_Fail()
    {
        var volunteer = CreateVolunteerWithPets(3);
        var pet = CreatePet();

        var result1 = volunteer.AddPet(pet);
        var result2 = volunteer.AddPet(pet);

        Assert.True(result1.IsSuccess);
        Assert.True(result2.IsFailure);
    }
    [Fact]
    public void Create_Volunteer_With_Three_Pet_Remove_Pet_At_Start_Should_ReSet_Serials()
    {
        var volunteer = CreateVolunteerWithPets(3);
        var petFirst = volunteer.Pets[0];
        var petSecond = volunteer.Pets[1];

        var result = volunteer.RemovePet(petFirst);

        var petExists = volunteer.GetPetById(petFirst.Id);

        Assert.True(result.IsSuccess);
        Assert.True(petExists.IsFailure);
        Assert.Equal(1, petSecond.PetPosition.Value);
    }
    [Fact]
    public void Create_Volunteer_With_Three_Pet_Move_Pet_At_Start_Should_ReSet_Serials()
    {
        var volunteer = CreateVolunteerWithPets(3);
        var petFirst = volunteer.Pets.First();
        var petLast = volunteer.Pets.Last();

        var result = volunteer.MovePetToStart(petLast);

        var petResult = volunteer.GetPetById(petLast.Id);

        Assert.True(result.IsSuccess);
        Assert.True(petResult.IsSuccess);
        Assert.Equal(2, petFirst.PetPosition.Value);
        Assert.Equal(1, petLast.PetPosition.Value);
    }
    [Fact]
    public void Create_Volunteer_With_Three_Pet_Move_Middle_To_End_Should_ReSet_Positions()
    {
        var volunteer = CreateVolunteerWithPets(3);
        var petMiddle = volunteer.Pets[1];
        var petLast = volunteer.Pets.Last();

        var result = volunteer.MovePetToEnd(petMiddle);

        var petResult = volunteer.GetPetById(petMiddle.Id);

        Assert.True(result.IsSuccess);
        Assert.True(petResult.IsSuccess);
        Assert.Equal(3, petMiddle.PetPosition.Value);
        Assert.Equal(2, petLast.PetPosition.Value);
    }
    private static Volunteer CreateVolunteerWithPets(int petQuantity)
    {
        var id = VolunteerId.NewVolunteerId();
        var fullName = FullName.Create("John", "Doe", "Smith").Value;
        var email = Email.Create("abobus@bobus.us").Value;
        var description = Description.Create("Description").Value;
        var phoneNumber = PhoneNumber.Create("+123457656789").Value;
        var experienceYears = ExperienceYears.Create(1).Value;

        var volunteer = Volunteer.Create(id, fullName, email, description, experienceYears, phoneNumber).Value;

        for (int i = 0; i < petQuantity; i++)
        {
            volunteer.AddPet(CreatePet());
        }

        return volunteer;
    }
    private static Pet CreatePet()
    {
        var id = PetId.NewPetId();
        var name = Name.Create("Dog").Value;
        var description = Description.Create("Description").Value;
        var type = PetType.Dog;
        var breedName = Name.Create("Breed").Value;
        var breed = PetBreed.Create(PetBreedId.NewPetBreedId(), breedName).Value;
        var specieName = Name.Create("Specie").Value;
        var specie = PetSpecie.Create(PetSpecieId.NewPetSpecieId(), specieName).Value;
        var color = Color.Create("Color").Value;
        var healthInfo = HealthInfo.Create("HealthInfo").Value;
        var address = Address.Create("Address", "Address", "Address", "Address", 2222).Value;
        var phoneNumber = PhoneNumber.Create("+123457656789").Value;
        var weight = WeightMeasurement.CreateFromGrams(1).Value;
        var height = HeightMeasurement.CreateFromCentimeters(1).Value;
        var birthDate = DateTime.Now;
        var isSterilized = true;
        var status = PetStatus.ShelterLooking;

        return Pet.Create(
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
            ).Value;
    }
}