using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;
public record PhoneNumber
{
    private PhoneNumber(
        uint countryCode,
        ulong number
        )
    {
        CountryCode = countryCode;
        Value = number;
    }

    public uint CountryCode { get; }
    public ulong Value { get; }

    public static Result<PhoneNumber, string> Create(
        uint countryCode,
        ulong number
        )
    {
        if (countryCode is < 1 or > 999)
            return "Country code should be between 1 and 999";

        if (number.ToString().Length is <= 7 or > 10)
            return "Phone number should be between  and 11 digits";

        return new PhoneNumber(countryCode, number);
    }
}
