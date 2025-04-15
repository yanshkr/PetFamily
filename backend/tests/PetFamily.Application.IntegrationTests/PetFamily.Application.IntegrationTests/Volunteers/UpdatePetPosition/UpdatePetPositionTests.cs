using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstraction;
using PetFamily.Volunteers.Application.Commands.UpdatePetPosition;
using PetFamily.Volunteers.Domain.Ids;

namespace PetFamily.Application.IntegrationTests.Volunteers.UpdatePetPosition;
public class UpdatePetPositionTests(VolunteersTestsWebFactory webFactory) : VolunteersBaseTests(webFactory)
{
    [Fact]
    public async Task Update_Pet_Position_Should_Be_Success()
    {
        // Arrange
        var specie = await SeedSpecieAsync();
        var breed = await SeedBreedAsync(specie);

        var volunteer = await SeedVolunteerAsync();
        var pet1 = await SeedPetAsync(volunteer, specie, breed);
        var pet2 = await SeedPetAsync(volunteer, specie, breed);
        var pet3 = await SeedPetAsync(volunteer, specie, breed);

        var command = new UpdatePetPositionCommand(volunteer.Id, pet2.Id, 1);

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<VolunteerId, UpdatePetPositionCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        _volunteersReadDbContext.Pets.FirstOrDefault(p => p.Id == pet2.Id && p.PetPosition == 1).Should().NotBeNull();
    }
}
