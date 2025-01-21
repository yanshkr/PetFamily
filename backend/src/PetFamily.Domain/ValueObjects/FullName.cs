using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;
public record FullName
{
    private FullName(
        string firstName,
        string middleName,
        string lastName
        )
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
    }

    public string FirstName { get; }
    public string MiddleName { get; }
    public string LastName { get; }

    public static Result<FullName, string> Create(
        string firstName,
        string middleName,
        string lastName
        )
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return "First name should not be empty";

        if (string.IsNullOrWhiteSpace(middleName))
            return "Middle name should not be empty";

        if (string.IsNullOrWhiteSpace(lastName))
            return "Last name should not be empty";

        if (firstName.Length is > 0 and < Constants.Shared.NAME_MAX_LENGTH)
            return $"First name should not be longer than {Constants.Shared.NAME_MAX_LENGTH} characters";

        if (middleName.Length is > 0 and < Constants.Shared.NAME_MAX_LENGTH)
            return $"Middle name should not be longer than {Constants.Shared.NAME_MAX_LENGTH} characters";

        if (lastName.Length is > 0 and < Constants.Shared.NAME_MAX_LENGTH)
            return $"Last name should not be longer than {Constants.Shared.NAME_MAX_LENGTH} characters";

        return new FullName(firstName, middleName, lastName);
    }
}
