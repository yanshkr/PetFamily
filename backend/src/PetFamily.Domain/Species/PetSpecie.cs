using CSharpFunctionalExtensions;
using PetFamily.Domain.Ids;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Species;
public class PetSpecie : BaseEntity<PetSpecieId>
{
    private readonly List<PetBreed> _breeds = [];
    private PetSpecie(
        PetSpecieId id,
        string name
        )
    {
        Id = id;
        Name = name;
    }

    public string Name { get; private set; }
    public IReadOnlyCollection<PetBreed> Breeds => _breeds;

    public static Result<PetSpecie, string> Create(
        string name
        )
    {
        if (string.IsNullOrWhiteSpace(name))
            return "Name should not be empty";

        return new PetSpecie(name);
    }
}
