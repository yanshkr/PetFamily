using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;
public record Description
{
    public const int MAX_DESCRIPTION_LENGTH = 500;

    private Description(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Description, Error> Create(
        string description
        )
    {
        if (string.IsNullOrWhiteSpace(description) || description.Length > MAX_DESCRIPTION_LENGTH)
            return Errors.General.ValueIsInvalid("Description");

        return new Description(description);
    }
}
