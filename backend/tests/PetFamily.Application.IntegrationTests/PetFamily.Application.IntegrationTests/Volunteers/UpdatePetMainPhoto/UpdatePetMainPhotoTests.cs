using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstraction;
using PetFamily.Volunteers.Application.Commands.UpdatePetMainPhoto;
using PetFamily.Volunteers.Domain.Ids;

namespace PetFamily.Application.IntegrationTests.Volunteers.UpdatePetMainPhoto;
public class UpdatePetMainPhotoTests(VolunteersTestsWebFactory webFactory) : VolunteersBaseTests(webFactory)
{
    [Fact]
    public async Task Update_Pet_Main_Photo_Should_Be_Success()
    {
        // Arrange
        var specie = await SeedSpecieAsync();
        var breed = await SeedBreedAsync(specie);

        var volunteer = await SeedVolunteerAsync();
        var pet = await SeedPetAsync(volunteer, specie, breed);

        await AddPhotosToPetAsync(pet);

        var command = new UpdatePetMainPhotoCommand(volunteer.Id, pet.Id, "photo2.png");

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<PetId, UpdatePetMainPhotoCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        _volunteersReadDbContext.Pets.FirstOrDefault(v => v.Id == pet.Id)!.MainPhoto.Should().Be("photo2.png");
    }
}
