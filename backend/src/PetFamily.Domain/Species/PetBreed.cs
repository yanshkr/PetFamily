using CSharpFunctionalExtensions;
using PetFamily.Domain.Ids;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Species;
public class PetBreed : BaseEntity<PetBreedId>
{
    private PetBreed(
        string name
        )
    {
        Name = name;
    }

    public string Name { get; private set; }

    public static Result<PetBreed, string> Create(
        string name
        )
    {
        if (string.IsNullOrWhiteSpace(name))
            return "Name should not be empty";

        return new PetBreed(name);
    }
}
