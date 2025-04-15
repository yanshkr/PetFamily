using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ErrorManagement;

namespace PetFamily.Volunteers.Domain.ValueObjects;
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
        int zipCode)
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
    public int ZipCode { get; }

    public static Result<Address, Error> Create(
        string country,
        string state,
        string city,
        string street,
        int zipCode)
    {
        if (string.IsNullOrWhiteSpace(country) || country.Length > MAX_VALUE_LENGTH)
            return Errors.General.ValueIsInvalid("Country");

        if (string.IsNullOrWhiteSpace(state) || state.Length > MAX_VALUE_LENGTH)
            return Errors.General.ValueIsInvalid("State");

        if (string.IsNullOrWhiteSpace(city) || city.Length > MAX_VALUE_LENGTH)
            return Errors.General.ValueIsInvalid("City");

        if (string.IsNullOrWhiteSpace(street) || street.Length > MAX_VALUE_LENGTH)
            return Errors.General.ValueIsInvalid("Street");

        if (zipCode is < MIN_ZIP_CODE_VALUE or > MAX_ZIP_CODE_VALUE)
            return Errors.General.ValueIsInvalid("ZipCode", MIN_ZIP_CODE_VALUE, MAX_ZIP_CODE_VALUE);

        return new Address(country, state, city, street, zipCode);
    }
}
