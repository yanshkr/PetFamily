using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Features.Commands.Species.AddBreed;
using PetFamily.Domain.Species.Ids;

namespace PetFamily.Application.IntegrationTests.Species.AddBreed;
public class AddBreedTests(IntegrationTestsWebFactory webFactory) : SpeciesBaseTests(webFactory)
{
    [Fact]
    public async Task Add_Breed_Should_Be_Success()
    {
        // Arrange
        var specie = await SeedSpecieAsync();
        var command = _fixture.BuildAddBreedCommand(specie.Id);

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<PetBreedId, AddBreedCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        _readDbContext.Breeds.FirstOrDefault().Should().NotBeNull();
    }
}
