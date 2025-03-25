using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Species;
using PetFamily.Domain.Species.Entities;
using PetFamily.Domain.Species.Ids;
using PetFamily.Domain.Volunteers;
using PetFamily.Domain.Volunteers.Entities;
using PetFamily.Domain.Volunteers.Enums;
using PetFamily.Domain.Volunteers.Ids;
using PetFamily.Domain.Volunteers.ValueObjects;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Application.IntegrationTests.Volunteers;
public class VolunteersBaseTests : IClassFixture<VolunteersTestsWebFactory>, IAsyncLifetime
{
    protected readonly VolunteersTestsWebFactory _factory;
    protected readonly IServiceScope _scope;
    protected readonly WriteDbContext _writeDbContext;
    protected readonly ReadDbContext _readDbContext;
    protected readonly IFixture _fixture;

    public VolunteersBaseTests(VolunteersTestsWebFactory factory)
    {
        _factory = factory;
        _scope = _factory.Services.CreateScope();
        _writeDbContext = _scope.ServiceProvider.GetRequiredService<WriteDbContext>();
        _readDbContext = _scope.ServiceProvider.GetRequiredService<ReadDbContext>();
        _fixture = new Fixture();
    }
    protected async Task<PetSpecie> SeedSpecieAsync()
    {
        var specieId = PetSpecieId.NewPetSpecieId();
        var name = Name.Create("Test").Value;

        var specie = PetSpecie.Create(specieId, name).Value;

        await _writeDbContext.PetSpecies.AddAsync(specie);

        await _writeDbContext.SaveChangesAsync();

        return specie;
    }
    protected async Task<PetBreed> SeedBreedAsync(PetSpecie specie)
    {
        var breedId = PetBreedId.NewPetBreedId();
        var name = Name.Create("Test").Value;

        var breed = PetBreed.Create(breedId, name).Value;

        specie.AddBreed(breed);

        await _writeDbContext.SaveChangesAsync();

        return breed;
    }
    protected async Task<Volunteer> SeedVolunteerAsync()
    {
        var volunteerId = VolunteerId.NewVolunteerId();
        var fullName = FullName.Create("Test", "Test", "Test").Value;
        var email = Email.Create("test@test.com").Value;
        var description = Description.Create("Test").Value;
        var experience = ExperienceYears.Create(10).Value;
        var phoneNumber = PhoneNumber.Create("+12345678911").Value;

        var volunteer = Volunteer.Create(volunteerId, fullName, email, description, experience, phoneNumber).Value;

        await _writeDbContext.Volunteers.AddAsync(volunteer);

        await _writeDbContext.SaveChangesAsync();

        return volunteer;
    }
    protected async Task<Pet> SeedPetAsync(Volunteer volunteer, PetSpecie petSpecie, PetBreed petBreed)
    {
        var petId = PetId.NewPetId();
        var name = Name.Create("Test").Value;
        var description = Description.Create("Test").Value;
        var breed = petBreed;
        var specie = petSpecie;
        var color = Color.Create("Test").Value;
        var healthInfo = HealthInfo.Create("Test").Value;
        var address = Address.Create("Test", "Test", "Test", "Test", 1234).Value;
        var phoneNumber = PhoneNumber.Create("+12345678911").Value;
        var weight = WeightMeasurement.CreateFromGrams(1).Value;
        var height = HeightMeasurement.CreateFromCentimeters(1).Value;

        var pet = Pet.Create(
            petId,
            name,
            description,
            PetType.Dog,
            breed,
            specie,
            color,
            healthInfo,
            address,
            phoneNumber,
            weight,
            height,
            DateTime.Now,
            true,
            PetStatus.ShelterLooking).Value;

        volunteer.AddPet(pet);

        await _writeDbContext.SaveChangesAsync();

        return pet;
    }
    protected async Task AddPhotosToPetAsync(Pet pet)
    {
        var photos = new List<Photo>
        {
            Photo.Create("photo1.png").Value,
            Photo.Create("photo2.png").Value
        };

        pet.AddPhotos(photos);

        await _writeDbContext.SaveChangesAsync();
    }
    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        _scope.Dispose();

        await _factory.ResetDatabaseAsync();
    }
}
