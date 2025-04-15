using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstraction;
using PetFamily.Species.Application.Commands.AddBreed;
using PetFamily.Species.Domain.Ids;

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

        _speciesReadDbContext.Breeds.FirstOrDefault().Should().NotBeNull();
    }
}
