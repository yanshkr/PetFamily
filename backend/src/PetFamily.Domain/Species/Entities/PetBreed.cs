using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species.Ids;

namespace PetFamily.Domain.Species.Entities;
public class PetBreed : BaseEntity<PetBreedId>
{
    public const int MAX_NAME_LENGTH = 100;

    public PetSpecie Specie { get; private set; } = null!;

#pragma warning disable CS8618
    private PetBreed() { }
#pragma warning restore CS8618

    private PetBreed(
        PetBreedId id,
        string name
        )
    {
        Id = id;
        Name = name;
    }

    public string Name { get; private set; }

    public static Result<PetBreed, Error> Create(
        PetBreedId id,
        string name
        )
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH)
            return Errors.General.ValueIsInvalid("Name");

        return new PetBreed(id, name);
    }
}
