using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Features.Commands.Volunteers.UpdatePetMainInfo;
using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.IntegrationTests.Volunteers.UpdatePetMainInfo;
public class UpdatePetMainInfoTests(VolunteersTestsWebFactory webFactory) : VolunteersBaseTests(webFactory)
{
    [Fact]
    public async Task Update_Pet_Main_Info_Should_Be_Success()
    {
        // Arrange
        var specie = await SeedSpecieAsync();
        var breed = await SeedBreedAsync(specie);

        var volunteer = await SeedVolunteerAsync();
        var pet = await SeedPetAsync(volunteer, specie, breed);

        var command = _fixture.BuildUpdatePetMainInfoCommand(volunteer.Id, pet.Id, specie.Id, breed.Id);

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<PetId, UpdatePetMainInfoCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        _readDbContext.Pets
            .FirstOrDefault(v => v.Id == pet.Id && v.Name == "updatedName")
            .Should().NotBeNull();
    }
}
