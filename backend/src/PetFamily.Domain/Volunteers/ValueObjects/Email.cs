using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System.Text.RegularExpressions;

namespace PetFamily.Domain.ValueObjects;
public record Email
{
    private const string EMAIL_REGEX = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
    public const int MAX_EMAIL_LENGTH = 100;

    private Email(
        string value
        )
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Email, Error> Create(
        string email
        )
    {
        if (!Regex.IsMatch(email, EMAIL_REGEX) || email.Length > MAX_EMAIL_LENGTH)
            return Errors.General.ValueIsInvalid("Email");

        return new Email(email);
    }
}
