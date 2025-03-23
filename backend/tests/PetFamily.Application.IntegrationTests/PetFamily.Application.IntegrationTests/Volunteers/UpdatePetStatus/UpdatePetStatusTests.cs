using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Features.Commands.Volunteers.UpdatePetStatus;
using PetFamily.Domain.Volunteers.Enums;
using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.IntegrationTests.Volunteers.UpdatePetStatus;
public class UpdatePetStatusTests(VolunteersTestsWebFactory webFactory) : VolunteersBaseTests(webFactory)
{
    [Fact]
    public async Task Update_Pet_Status_Should_Be_Success()
    {
        // Arrange
        var specie = await SeedSpecieAsync();
        var breed = await SeedBreedAsync(specie);

        var volunteer = await SeedVolunteerAsync();
        var pet = await SeedPetAsync(volunteer, specie, breed);

        var command = new UpdatePetStatusCommand(volunteer.Id, pet.Id, PetStatus.ShelterFound);

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<PetId, UpdatePetStatusCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        _readDbContext.Pets.FirstOrDefault(p => p.Id == pet.Id && p.Status == PetStatus.ShelterFound).Should().NotBeNull();
    }
}
