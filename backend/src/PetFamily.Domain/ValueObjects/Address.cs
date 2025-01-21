using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;
public record Address
{
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
        if (string.IsNullOrWhiteSpace(country))
            return "Country should not be empty";

        if (string.IsNullOrWhiteSpace(state))
            return "State should not be empty";

        if (string.IsNullOrWhiteSpace(city))
            return "City should not be empty";

        if (string.IsNullOrWhiteSpace(street))
            return "Street should not be empty";

        if (zipCode is > 0 and < 99999)
            return "ZipCode should not be empty";

        return new Address(country, state, city, street, zipCode);
    }

    public override string ToString()
    {
        return $"{Street}, {City}, {State}, {Country}, {ZipCode}";
    }
    public static Address FromString(string address)
    {
        var parts = address.Split(", ");
        return new Address(parts[3], parts[2], parts[1], parts[0], uint.Parse(parts[4]));
    }
}
