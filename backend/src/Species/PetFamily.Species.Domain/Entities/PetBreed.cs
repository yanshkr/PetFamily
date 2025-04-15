using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.Entities;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Species.Domain.Ids;

namespace PetFamily.Species.Domain.Entities;
public class PetBreed : BaseEntity<PetBreedId>
{
    public const int MAX_NAME_LENGTH = 100;

    public PetSpecie Specie { get; private set; } = null!;

#pragma warning disable CS8618
    private PetBreed() { }
#pragma warning restore CS8618

    private PetBreed(PetBreedId id, Name name)
    {
        Id = id;
        Name = name;
    }

    public Name Name { get; private set; }

    public static Result<PetBreed, Error> Create(PetBreedId id, Name name)
    {
        return new PetBreed(id, name);
    }
}
