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

    public static Result<FullName, Error> Create(
        string firstName,
        string middleName,
        string lastName
        )
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > MAX_VALUE_LENGTH)
            return Errors.General.ValueIsInvalid("FirstName");

        if (string.IsNullOrWhiteSpace(middleName) || middleName.Length > MAX_VALUE_LENGTH)
            return Errors.General.ValueIsInvalid("MiddleName");

        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > MAX_VALUE_LENGTH)
            return Errors.General.ValueIsInvalid("LastName");

        return new FullName(firstName, middleName, lastName);
    }
}
