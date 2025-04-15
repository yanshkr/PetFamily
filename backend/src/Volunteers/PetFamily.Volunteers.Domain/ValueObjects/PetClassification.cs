using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ErrorManagement;
using System.Text.Json.Serialization;

namespace PetFamily.Volunteers.Domain.ValueObjects;
public record PetClassification
{

    [JsonConstructor]
    private PetClassification(Guid breedId, Guid specieId)
    {
        BreedId = breedId;
        SpecieId = specieId;
    }

    public Guid BreedId { get; }
    public Guid SpecieId { get; }

    public static Result<PetClassification, Error> Create(Guid breedId, Guid specieId)
    {
        if (breedId == Guid.Empty)
            return Errors.General.ValueIsInvalid("BreedId");

        if (specieId == Guid.Empty)
            return Errors.General.ValueIsInvalid("SpecieId");

        return new PetClassification(breedId, specieId);
    }
}
