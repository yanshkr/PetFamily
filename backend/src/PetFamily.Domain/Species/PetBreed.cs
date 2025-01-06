using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Species;
public class PetBreed : BaseEntity<Guid>
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

    public Result UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure("Name should not be empty");

        Name = name;

        return Result.Success();
    }
}
