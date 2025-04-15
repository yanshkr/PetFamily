using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstraction;
using PetFamily.Species.Application.Commands.DeleteBreed;
using PetFamily.Species.Domain.Ids;

namespace PetFamily.Application.IntegrationTests.Species.DeleteBreed;
public class DeleteBreedTests(IntegrationTestsWebFactory factory) : SpeciesBaseTests(factory)
{
    [Fact]
    public async Task Delete_Breed_Should_Be_Success()
    {
        // Arrange
        var specie = await SeedSpecieAsync();
        var breed = await SeedBreedAsync(specie);

        var command = new DeleteBreedCommand(specie.Id, breed.Id);

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<PetBreedId, DeleteBreedCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        _speciesReadDbContext.Breeds.FirstOrDefault().Should().BeNull();
    }
    [Fact]
    public async Task Delete_Breed_With_Existing_Pet_Should_Be_Fail()
    {
        // Arrange
        var specie = await SeedSpecieAsync();
        var breed = await SeedBreedAsync(specie);

        var volunteer = await SeedVolunteerAsync();
        var pet = await SeedPetAsync(volunteer, specie, breed);

        var command = new DeleteBreedCommand(specie.Id, breed.Id);

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<PetBreedId, DeleteBreedCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();

        _speciesReadDbContext.Breeds.FirstOrDefault().Should().NotBeNull();
        _volunteersReadDbContext.Pets.FirstOrDefault().Should().NotBeNull();
    }
    [Fact]
    public async Task Delete_Breed_With_Invalid_Id_Should_Fail()
    {
        // Arrange
        var command = new DeleteBreedCommand(PetSpecieId.NewPetSpecieId(), PetBreedId.NewPetBreedId());

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<PetBreedId, DeleteBreedCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();
    }
}