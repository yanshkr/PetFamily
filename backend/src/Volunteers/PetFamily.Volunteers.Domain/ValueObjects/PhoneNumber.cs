using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ErrorManagement;
using System.Text.RegularExpressions;

namespace PetFamily.Volunteers.Domain.ValueObjects;
public record PhoneNumber
{
    private const string PHONE_NUMBER_REGEX = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";
    public const int MAX_PHONE_NUMBER_LENGTH = 15;

    private PhoneNumber(
        string value
        )
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<PhoneNumber, Error> Create(string number)
    {
        if (!Regex.IsMatch(number, PHONE_NUMBER_REGEX) || number.Length > MAX_PHONE_NUMBER_LENGTH)
            return Errors.General.ValueIsInvalid("PhoneNumber");

        return new PhoneNumber(number);
    }
}
