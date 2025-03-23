using AutoFixture;
using PetFamily.Application.Features.Commands.Species.AddBreed;
using PetFamily.Application.Features.Commands.Species.Create;

namespace PetFamily.Application.IntegrationTests.Species;
public static class SpeciesFixtureExtensions
{
    public static CreateSpecieCommand BuildCreateSpecieCommand(
        this IFixture fixture)
    {
        return fixture.Build<CreateSpecieCommand>()
            .Create();
    }
    public static AddBreedCommand BuildAddBreedCommand(
        this IFixture fixture,
        Guid specieId)
    {
        return fixture.Build<AddBreedCommand>()
            .With(x => x.SpecieId, specieId)
            .Create();
    }
}