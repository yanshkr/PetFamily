using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;
public record FullName
{
    public const int MAX_VALUE_LENGTH = 100;

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
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length <= MAX_VALUE_LENGTH)
            return "First name should not be empty";

        if (string.IsNullOrWhiteSpace(middleName) || middleName.Length <= MAX_VALUE_LENGTH)
            return "Middle name should not be empty";

        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length <= MAX_VALUE_LENGTH)
            return "Last name should not be empty";

        return new FullName(firstName, middleName, lastName);
    }
}
