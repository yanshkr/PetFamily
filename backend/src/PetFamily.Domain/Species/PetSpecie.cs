using CSharpFunctionalExtensions;
using PetFamily.Domain.Ids;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Species;
public class PetSpecie : BaseEntity<PetSpecieId>
{
    private readonly List<PetBreed> _breeds = [];

    private PetSpecie() { }

    private PetSpecie(
        PetSpecieId id,
        string name
        )
    {
        Id = id;
        Name = name;
    }

    public string Name { get; private set; }
    public IReadOnlyList<PetBreed> Breeds => _breeds;

    public static Result<PetSpecie, string> Create(
        PetSpecieId id,
        string name
        )
    {
        if (string.IsNullOrWhiteSpace(name))
            return "Name should not be empty";

        return new PetSpecie(id, name);
    }
}
