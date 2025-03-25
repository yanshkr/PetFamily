using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Features.Commands.Volunteers.DeletePhotosAtPet;
using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.IntegrationTests.Volunteers.DeletePhotosAtPet;
public class DeletePhotosAtPetTests(VolunteersTestsWebFactory webFactory) : VolunteersBaseTests(webFactory)
{
    [Fact]
    public async Task Delete_Photos_At_Pet_Should_Be_Success()
    {
        // Arrange
        var specie = await SeedSpecieAsync();
        var breed = await SeedBreedAsync(specie);

        var volunteer = await SeedVolunteerAsync();
        var pet = await SeedPetAsync(volunteer, specie, breed);

        await AddPhotosToPetAsync(pet);

        var command = new DeletePetPhotosCommand(volunteer.Id, pet.Id, ["photo1.png", "photo2.png"]);

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<IEnumerable<string>, DeletePetPhotosCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        _readDbContext.Pets.FirstOrDefault(p => p.Id == pet.Id)!.Photos.Count.Should().Be(0);
    }
}
