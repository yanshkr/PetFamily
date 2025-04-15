using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstraction;
using PetFamily.Volunteers.Application.Commands.UpdateVolunteerSocialMedia;
using PetFamily.Volunteers.Domain.Ids;

namespace PetFamily.Application.IntegrationTests.Volunteers.UpdateVolunteerSocialMedia;
public class UpdateVolunteerSocialMediaTests(VolunteersTestsWebFactory webFactory) : VolunteersBaseTests(webFactory)
{
    [Fact]
    public async Task Update_Volunteer_Payment_Info_Should_Be_Success()
    {
        // Arrange
        var volunteer = await SeedVolunteerAsync();

        var command = _fixture.BuildUpdateVolunteerSocialMediaCommand(volunteer.Id);

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<VolunteerId, UpdateVolunteerSocialMediaCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        var volunteerFromDb = _volunteersReadDbContext.Volunteers.FirstOrDefault(v => v.Id == volunteer.Id);
        volunteerFromDb!.SocialMedias.FirstOrDefault(vb => vb.Name == "updatedSocialName").Should().NotBeNull();
    }
}
