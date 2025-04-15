using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstraction;
using PetFamily.Volunteers.Application.Commands.UpdateVolunteerPaymentInfo;
using PetFamily.Volunteers.Domain.Ids;

namespace PetFamily.Application.IntegrationTests.Volunteers.UpdateVolunteerPaymentInfo;
public class UpdateVolunteerPaymentInfoTests(VolunteersTestsWebFactory webFactory) : VolunteersBaseTests(webFactory)
{
    [Fact]
    public async Task Update_Volunteer_Payment_Info_Should_Be_Success()
    {
        // Arrange
        var volunteer = await SeedVolunteerAsync();

        var command = _fixture.BuildUpdateVolunteerPaymentInfoCommand(volunteer.Id);

        var sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<VolunteerId, UpdateVolunteerPaymentInfoCommand>>();

        // Act
        var result = await sut.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        var volunteerFromDb = _volunteersReadDbContext.Volunteers.FirstOrDefault(v => v.Id == volunteer.Id);
        volunteerFromDb!.PaymentInfos.FirstOrDefault(vb => vb.Name == "updatedPaymentName").Should().NotBeNull();
    }
}
