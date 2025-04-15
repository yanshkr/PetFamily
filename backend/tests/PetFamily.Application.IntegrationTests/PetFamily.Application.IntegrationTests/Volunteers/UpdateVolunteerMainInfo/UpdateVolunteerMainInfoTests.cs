using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstraction;
using PetFamily.Volunteers.Application.Commands.UpdateVolunteerMainInfo;
using PetFamily.Volunteers.Domain.Ids;

namespace PetFamily.Application.IntegrationTests.Volunteers.UpdateVolunteerMainInfo;
public class UpdateVolunteerMainInfoTests(VolunteersTestsWebFactory webFactory) : VolunteersBaseTests(webFactory)
{
    [Fact]
    public async Task Update_Volunteer_Main_Info_Should_Be_Success()
    {
        // Arrange
        var volunteer = await SeedVolunteerAsync();

        var command = _fixture.BuildUpdateVolunteerMainInfoCommand(volunteer.Id);

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<VolunteerId, UpdateVolunteerCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        _volunteersReadDbContext.Volunteers
            .FirstOrDefault(v => v.Id == volunteer.Id && v.FirstName == "updatedName")
            .Should().NotBeNull();
    }
}
