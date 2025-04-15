using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ErrorManagement;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace PetFamily.Volunteers.Domain.ValueObjects;
public record PaymentInfo
{
    private const string CREDIT_CARD_REGEX = @"^(\d{4}[- ]?){3}\d{4}$";
    public const int MAX_NAME_LENGTH = 100;

    [JsonConstructor]
    private PaymentInfo(string name, string address)
    {
        Name = name;
        Address = address;
    }

    public string Name { get; }
    public string Address { get; }

    public static Result<PaymentInfo, Error> Create(string name, string address)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH)
            return Errors.General.ValueIsInvalid("Name");

        if (!Regex.IsMatch(address, CREDIT_CARD_REGEX))
            return Errors.General.ValueIsInvalid("Address");

        return new PaymentInfo(name, address);
    }
}
