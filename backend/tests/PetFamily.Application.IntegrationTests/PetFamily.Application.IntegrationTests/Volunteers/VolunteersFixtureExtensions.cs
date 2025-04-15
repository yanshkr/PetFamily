using AutoFixture;
using PetFamily.Volunteers.Application.Commands.AddPet;
using PetFamily.Volunteers.Application.Commands.CreateVolunteer;
using PetFamily.Volunteers.Application.Commands.UpdatePetMainInfo;
using PetFamily.Volunteers.Application.Commands.UpdateVolunteerMainInfo;
using PetFamily.Volunteers.Application.Commands.UpdateVolunteerPaymentInfo;
using PetFamily.Volunteers.Application.Commands.UpdateVolunteerSocialMedia;
using PetFamily.Volunteers.Contracts.Dtos.Shared;
using PetFamily.Volunteers.Contracts.Dtos.Volunteer;

namespace PetFamily.Application.IntegrationTests.Volunteers;
public static class VolunteersFixtureExtensions
{
    public static AddPetCommand BuildAddPetCommand(
        this IFixture fixture,
        Guid volunteerId,
        Guid specieId,
        Guid breedId)
    {
        return fixture.Build<AddPetCommand>()
            .With(x => x.VolunteerId, volunteerId)
            .With(x => x.SpecieId, specieId)
            .With(x => x.BreedId, breedId)
            .With(x => x.PhoneNumber, "+12345678911")
            .With(x => x.Height, 10)
            .With(x => x.Weight, 10)
            .Create();
    }
    public static CreateVolunteerCommand BuildCreateVolunteerCommand(
        this IFixture fixture)
    {
        return fixture.Build<CreateVolunteerCommand>()
            .With(x => x.PhoneNumber, "+12345678911")
            .With(x => x.Experience, 10)
            .With(x => x.Email, "test@test.com")
            .Create();
    }
    public static UpdateVolunteerCommand BuildUpdateVolunteerMainInfoCommand(
        this IFixture fixture,
        Guid volunteerId)
    {
        return fixture.Build<UpdateVolunteerCommand>()
            .With(x => x.Id, volunteerId)
            .With(x => x.FirstName, "updatedName")
            .With(x => x.PhoneNumber, "+12345678911")
            .With(x => x.Email, "test@test.com")
            .With(x => x.Experience, 10)
            .Create();
    }
    public static UpdatePetMainInfoCommand BuildUpdatePetMainInfoCommand(
        this IFixture fixture,
        Guid volunteerId,
        Guid petId,
        Guid speciesId,
        Guid breedId)
    {
        return fixture.Build<UpdatePetMainInfoCommand>()
            .With(x => x.VolunteerId, volunteerId)
            .With(x => x.PetId, petId)
            .With(x => x.SpecieId, speciesId)
            .With(x => x.BreedId, breedId)
            .With(x => x.Name, "updatedName")
            .With(x => x.PhoneNumber, "+12345678911")
            .With(x => x.Height, 10)
            .With(x => x.Weight, 10)
            .Create();
    }
    public static UpdateVolunteerPaymentInfoCommand BuildUpdateVolunteerPaymentInfoCommand(
        this IFixture fixture,
        Guid volunteerId)
    {
        return fixture.Build<UpdateVolunteerPaymentInfoCommand>()
            .With(x => x.Id, volunteerId)
            .With(x => x.PaymentInfos,
            [
                new PaymentInfoDto
                {
                    Name = "updatedPaymentName",
                    Address = "1111111111111111"
                }
            ])
            .Create();
    }
    public static UpdateVolunteerSocialMediaCommand BuildUpdateVolunteerSocialMediaCommand(
        this IFixture fixture,
        Guid volunteerId)
    {
        return fixture.Build<UpdateVolunteerSocialMediaCommand>()
            .With(x => x.Id, volunteerId)
            .With(x => x.SocialMedias,
            [
                new SocialMediaDto
                {
                    Url = "http://test.com",
                    Name = "updatedSocialName",
                }
            ])
            .Create();
    }
}
