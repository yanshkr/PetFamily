using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;
public record Address
{
    public const int MAX_VALUE_LENGTH = 200;

    public const int MIN_ZIP_CODE_VALUE = 1;
    public const int MAX_ZIP_CODE_VALUE = 99999;

    private Address(
        string country,
        string state,
        string city,
        string street,
        uint zipCode
        )
    {
        Country = country;
        State = state;
        City = city;
        Street = street;
        ZipCode = zipCode;
    }

    public string Country { get; }
    public string State { get; }
    public string City { get; }
    public string Street { get; }
    public uint ZipCode { get; }

    public static Result<Address, string> Create(
        string country,
        string state,
        string city,
        string street,
        uint zipCode
        )
    {
        if (string.IsNullOrWhiteSpace(country) || country.Length <= MAX_VALUE_LENGTH)
            return $"Country should not be longer than {MAX_VALUE_LENGTH} characters";

        if (string.IsNullOrWhiteSpace(state) || state.Length <= MAX_VALUE_LENGTH)
            return $"State should not be longer than {MAX_VALUE_LENGTH} characters";

        if (string.IsNullOrWhiteSpace(city) || city.Length <= MAX_VALUE_LENGTH)
            return $"City should not be longer than {MAX_VALUE_LENGTH} characters";

        if (string.IsNullOrWhiteSpace(street) || street.Length <= MAX_VALUE_LENGTH)
            return $"Street should not be longer than {MAX_VALUE_LENGTH} characters";

        if (zipCode is >= MIN_ZIP_CODE_VALUE and < MAX_ZIP_CODE_VALUE)
            return "ZipCode should not be empty";

        return new Address(country, state, city, street, zipCode);
    }
}
