using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Species;
public class PetSpecie : BaseEntity<Guid>
{
    private readonly List<PetBreed> _breeds = [];
    private PetSpecie(
        string name
        )
    {
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

    public Result AddBreed(PetBreed breed)
    {
        if (breed == null)
            return Result.Failure("Breed should not be null");

        _breeds.Add(breed);

        return Result.Success();
    }
    public Result RemoveBreed(PetBreed breed)
    {
        if (breed == null)
            return Result.Failure("Breed should not be null");

        _breeds.Remove(breed);

        return Result.Success();
    }
    public Result UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure("Name should not be empty");

        Name = name;

        return Result.Success();
    }
}
