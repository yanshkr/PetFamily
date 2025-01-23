using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace PetFamily.Domain.ValueObjects;
public record Email
{
    private const string EMAIL_REGEX = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2}$";
    public const int MAX_EMAIL_LENGTH = 100;

    private Email(
        string value
        )
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Email, string> Create(
        string email
        )
    {
        if (!Regex.IsMatch(email, EMAIL_REGEX) || email.Length <= MAX_EMAIL_LENGTH)
            return "Email is not valid";

        return new Email(email);
    }
}
