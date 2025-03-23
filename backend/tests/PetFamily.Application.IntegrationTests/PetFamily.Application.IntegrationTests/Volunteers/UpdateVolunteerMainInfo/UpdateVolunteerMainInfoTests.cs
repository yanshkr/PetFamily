﻿using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Features.Commands.Volunteers.UpdateVolunteerMainInfo;
using PetFamily.Domain.Volunteers.Ids;

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

        _readDbContext.Volunteers
            .FirstOrDefault(v => v.Id == volunteer.Id && v.FirstName == "updatedName")
            .Should().NotBeNull();
    }
}
