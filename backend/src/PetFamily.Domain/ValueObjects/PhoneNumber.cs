using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;
public record PhoneNumber
{
    private PhoneNumber(
        string value
        )
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<PhoneNumber, string> Create(
        string number
        )
    {
        if (number.ToString().Length is <= 7 or > 10)
            return "Phone number should be between  and 11 digits";

        return new PhoneNumber(number);
    }
}
