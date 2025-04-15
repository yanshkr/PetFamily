using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Species.Domain.Entities;
using PetFamily.Species.Domain.Ids;
using PetFamily.Volunteers.Domain.Entities;
using PetFamily.Volunteers.Domain.Enums;
using PetFamily.Volunteers.Domain.Ids;
using PetFamily.Volunteers.Domain.ValueObjects;
using PetFamily.Volunteers.Infrastructure.DbContexts;

namespace PetFamily.Application.IntegrationTests.Volunteers;
public class VolunteersBaseTests : IClassFixture<VolunteersTestsWebFactory>, IAsyncLifetime
{
    protected readonly VolunteersTestsWebFactory _factory;
    protected readonly IServiceScope _scope;
    protected readonly VolunteersWriteDbContext _volunteersWriteDbContext;
    protected readonly VolunteersReadDbContext _volunteersReadDbContext;
    protected readonly SpeciesWriteDbContext _speciesWriteDbContext;
    protected readonly SpeciesReadDbContext _speciesReadDbContext;
    protected readonly IFixture _fixture;

    public VolunteersBaseTests(VolunteersTestsWebFactory factory)
    {
        _factory = factory;
        _scope = _factory.Services.CreateScope();
        _volunteersWriteDbContext = _scope.ServiceProvider.GetRequiredService<VolunteersWriteDbContext>();
        _volunteersReadDbContext = _scope.ServiceProvider.GetRequiredService<VolunteersReadDbContext>();
        _speciesWriteDbContext = _scope.ServiceProvider.GetRequiredService<SpeciesWriteDbContext>();
        _speciesReadDbContext = _scope.ServiceProvider.GetRequiredService<SpeciesReadDbContext>();
        _fixture = new Fixture();
    }
    protected async Task<PetSpecie> SeedSpecieAsync()
    {
        var specieId = PetSpecieId.NewPetSpecieId();
        var name = Name.Create("Test").Value;

        var specie = PetSpecie.Create(specieId, name).Value;

        await _speciesWriteDbContext.PetSpecies.AddAsync(specie);
        await _speciesWriteDbContext.SaveChangesAsync();

        return specie;
    }
    protected async Task<PetBreed> SeedBreedAsync(PetSpecie specie)
    {
        var breedId = PetBreedId.NewPetBreedId();
        var name = Name.Create("Test").Value;

        var breed = PetBreed.Create(breedId, name).Value;

        specie.AddBreed(breed);

        await _speciesWriteDbContext.SaveChangesAsync();

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

        await _volunteersWriteDbContext.Volunteers.AddAsync(volunteer);
        await _volunteersWriteDbContext.SaveChangesAsync();

        return volunteer;
    }
    protected async Task<Pet> SeedPetAsync(Volunteer volunteer, PetSpecie petSpecie, PetBreed petBreed)
    {
        var petId = PetId.NewPetId();
        var name = Name.Create("Test").Value;
        var description = Description.Create("Test").Value;
        var classification = PetClassification.Create(petBreed.Id, petSpecie.Id).Value;
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
            classification,
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

        await _volunteersWriteDbContext.SaveChangesAsync();

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

        await _volunteersWriteDbContext.SaveChangesAsync();
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
