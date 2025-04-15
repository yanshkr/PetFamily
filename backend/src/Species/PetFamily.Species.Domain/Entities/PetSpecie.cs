using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.Entities;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Species.Domain.Ids;

namespace PetFamily.Species.Domain.Entities;
public class PetSpecie : BaseEntity<PetSpecieId>
{
    public const int MAX_NAME_LENGTH = 100;

    private readonly List<PetBreed> _breeds = [];

#pragma warning disable CS8618
    private PetSpecie() { }
#pragma warning restore CS8618

    private PetSpecie(PetSpecieId id, Name name)
    {
        Id = id;
        Name = name;
    }

    public Name Name { get; private set; }
    public IReadOnlyList<PetBreed> Breeds => _breeds;

    public static Result<PetSpecie, Error> Create(PetSpecieId id, Name name)
    {
        return new PetSpecie(id, name);
    }

    public Result<PetBreed, Error> GetBreed(PetBreedId petBreedId)
    {
        var breed = _breeds.FirstOrDefault(b => b.Id == petBreedId);

        if (breed is null)
            return Errors.General.NotFound(petBreedId);

        return breed;
    }
    public void AddBreed(PetBreed breed)
    {
        _breeds.Add(breed);
    }
    public void RemoveBreed(PetBreed breed)
    {
        _breeds.Remove(breed);
    }
}
