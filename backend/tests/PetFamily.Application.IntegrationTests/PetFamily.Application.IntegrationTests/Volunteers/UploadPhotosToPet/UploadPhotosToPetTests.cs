using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Dtos;
using PetFamily.Application.Features.Commands.Volunteers.UploadPhotosToPet;

namespace PetFamily.Application.IntegrationTests.Volunteers.UploadPhotosToPet;
public class UploadPhotosToPetTests(VolunteersTestsWebFactory webFactory) : VolunteersBaseTests(webFactory)
{
    [Fact]
    public async Task Upload_Photos_To_Pet_Should_Be_Success()
    {
        // Arrange
        var specie = await SeedSpecieAsync();
        var breed = await SeedBreedAsync(specie);

        var volunteer = await SeedVolunteerAsync();
        var pet = await SeedPetAsync(volunteer, specie, breed);

        var command = new UploadPetPhotosCommand(volunteer.Id, pet.Id, [new UploadFileDto(Stream.Null, "test.png")]);

        _factory.SetupFileProviderSuccessUploadMock();

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<IEnumerable<string>, UploadPetPhotosCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        _readDbContext.Pets.FirstOrDefault(v => v.Id == pet.Id).Should().NotBeNull();
        _readDbContext.Pets.FirstOrDefault(v => v.Id == pet.Id)!.Photos.Count.Should().Be(2);
    }
    [Fact]
    public async Task Upload_Photos_To_Pet_Should_Be_Fail()
    {
        // Arrange
        var specie = await SeedSpecieAsync();
        var breed = await SeedBreedAsync(specie);

        var volunteer = await SeedVolunteerAsync();
        var pet = await SeedPetAsync(volunteer, specie, breed);

        var command = new UploadPetPhotosCommand(volunteer.Id, pet.Id, [new UploadFileDto(Stream.Null, "test.png")]);

        _factory.SetupFileProviderFailedUploadMock();

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<IEnumerable<string>, UploadPetPhotosCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeFalse();

        _readDbContext.Pets.FirstOrDefault(v => v.Id == pet.Id).Should().NotBeNull();
        _readDbContext.Pets.FirstOrDefault(v => v.Id == pet.Id)!.Photos.Count.Should().Be(0);
    }
}
