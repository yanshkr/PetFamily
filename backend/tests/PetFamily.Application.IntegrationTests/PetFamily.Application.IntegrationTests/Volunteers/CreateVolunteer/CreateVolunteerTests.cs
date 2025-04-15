using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstraction;
using PetFamily.Volunteers.Application.Commands.CreateVolunteer;
using PetFamily.Volunteers.Domain.Ids;

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

        _volunteersReadDbContext.Volunteers.FirstOrDefault().Should().NotBeNull();
    }
}
