using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Features.Commands.Species.Delete;
using PetFamily.Domain.Species.Ids;

namespace PetFamily.Application.IntegrationTests.Species.DeleteSpecie;
public class DeleteSpecieTests(IntegrationTestsWebFactory factory) : SpeciesBaseTests(factory)
{
    [Fact]
    public async Task Delete_Specie_Should_Be_Success()
    {
        // Arrange
        var specie = await SeedSpecieAsync();
        var command = new DeleteSpecieCommand(specie.Id.Value);

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<PetSpecieId, DeleteSpecieCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(specie.Id);

        _readDbContext.Species.FirstOrDefault().Should().BeNull();
    }
    [Fact]
    public async Task Delete_Specie_With_Existing_Pet_Should_Be_Fail()
    {
        // Arrange
        var specie = await SeedSpecieAsync();
        var breed = await SeedBreedAsync(specie);

        var volunteer = await SeedVolunteerAsync();
        var pet = await SeedPetAsync(volunteer, specie, breed);

        var command = new DeleteSpecieCommand(specie.Id.Value);

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<PetSpecieId, DeleteSpecieCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();

        _readDbContext.Species.FirstOrDefault().Should().NotBeNull();
        _readDbContext.Pets.FirstOrDefault().Should().NotBeNull();
    }
    [Fact]
    public async Task Delete_Specie_With_Invalid_Id_Should_Fail()
    {
        // Arrange
        var command = new DeleteSpecieCommand(PetSpecieId.NewPetSpecieId().Value);

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<PetSpecieId, DeleteSpecieCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();
    }
}
