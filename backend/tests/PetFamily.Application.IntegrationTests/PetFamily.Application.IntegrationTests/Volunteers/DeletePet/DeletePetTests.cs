using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstraction;
using PetFamily.Volunteers.Application.Commands.DeletePet;
using PetFamily.Volunteers.Domain.Ids;

namespace PetFamily.Application.IntegrationTests.Volunteers.DeletePet;
public class DeletePetTests(VolunteersTestsWebFactory webFactory) : VolunteersBaseTests(webFactory)
{
    [Fact]
    public async Task Delete_Pet_Hard_Should_Be_Success()
    {
        // Arrange
        var specie = await SeedSpecieAsync();
        var breed = await SeedBreedAsync(specie);

        var volunteer = await SeedVolunteerAsync();
        var pet = await SeedPetAsync(volunteer, specie, breed);

        var command = new DeletePetCommand(volunteer.Id, pet.Id, false);

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<PetId, DeletePetCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        _volunteersReadDbContext.Pets.FirstOrDefault().Should().BeNull();
    }
    [Fact]
    public async Task Delete_Pet_Soft_Should_Be_Success()
    {
        // Arrange
        var specie = await SeedSpecieAsync();
        var breed = await SeedBreedAsync(specie);

        var volunteer = await SeedVolunteerAsync();
        var pet = await SeedPetAsync(volunteer, specie, breed);

        var command = new DeletePetCommand(volunteer.Id, pet.Id, true);

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<PetId, DeletePetCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        _volunteersWriteDbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefault(v => v.Id == volunteer.Id)!
            .Pets.FirstOrDefault(p => p.Id == pet.Id && p.IsDeleted)
            .Should().NotBeNull();
    }
}
