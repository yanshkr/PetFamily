using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Features.Commands.Volunteers.CreateVolunteer;
using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.IntegrationTests.Volunteers.CreateVolunteer;
public class CreateVolunteerTests(VolunteersTestsWebFactory webFactory) : VolunteersBaseTests(webFactory)
{
    [Fact]
    public async Task Create_Volunteer_Should_Return_Success()
    {
        // Arrange
        var command = _fixture.BuildCreateVolunteerCommand();

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<VolunteerId, CreateVolunteerCommand>>();
        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        _readDbContext.Volunteers.FirstOrDefault().Should().NotBeNull();
    }
}
