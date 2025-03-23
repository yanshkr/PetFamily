using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Features.Commands.Volunteers.DeleteVolunteer;
using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.IntegrationTests.Volunteers.DeleteVolunteer;
public class DeleteVolunteerTests(VolunteersTestsWebFactory webFactory) : VolunteersBaseTests(webFactory)
{
    [Fact]
    public async Task Delete_Volunteer_Hard_Should_Be_Success()
    {
        // Arrange
        var volunteer = await SeedVolunteerAsync();

        var command = new DeleteVolunteerCommand(volunteer.Id, false);

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<VolunteerId, DeleteVolunteerCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        _readDbContext.Volunteers.FirstOrDefault().Should().BeNull();
    }
    [Fact]
    public async Task Delete_Volunteer_Soft_Should_Be_Success()
    {
        // Arrange
        var volunteer = await SeedVolunteerAsync();

        var command = new DeleteVolunteerCommand(volunteer.Id, true);

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<VolunteerId, DeleteVolunteerCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        _writeDbContext.Volunteers
            .FirstOrDefault(v => v.Id == volunteer.Id && v.IsDeleted)
            .Should().NotBeNull();
    }
}
