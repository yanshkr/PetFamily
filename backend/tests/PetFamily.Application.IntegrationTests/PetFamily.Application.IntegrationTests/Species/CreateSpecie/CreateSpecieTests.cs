using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Features.Commands.Species.Create;
using PetFamily.Domain.Species.Ids;

namespace PetFamily.Application.IntegrationTests.Species.CreateSpecie;
public class CreateSpecieTests(IntegrationTestsWebFactory factory) : SpeciesBaseTests(factory)
{
    [Fact]
    public async Task Create_Specie_Should_Be_Success()
    {
        // Arrange
        var command = _fixture.BuildCreateSpecieCommand();

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<PetSpecieId, CreateSpecieCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        _readDbContext.Species.FirstOrDefault().Should().NotBeNull();
    }
}
