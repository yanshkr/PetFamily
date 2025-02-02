using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species.Entities;
using PetFamily.Domain.Species.Ids;

namespace PetFamily.Domain.Species;
public class PetSpecie : BaseEntity<PetSpecieId>
{
    public const int MAX_NAME_LENGTH = 100;

    private readonly List<PetBreed> _breeds = [];

#pragma warning disable CS8618
    private PetSpecie() { }
#pragma warning restore CS8618

    private PetSpecie(PetSpecieId id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Name { get; private set; }
    public IReadOnlyList<PetBreed> Breeds => _breeds;

    public static Result<PetSpecie, Error> Create(PetSpecieId id, string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH)
            return Errors.General.ValueIsInvalid("Name");

        return new PetSpecie(id, name);
    }
}
