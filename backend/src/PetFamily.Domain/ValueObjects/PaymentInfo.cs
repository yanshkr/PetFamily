using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace PetFamily.Domain.ValueObjects;
public record PaymentInfo
{
    private const string CREDIT_CARD_REGEX = @"^(\d{4}[- ]?){3}\d{4}$";
    public const int MAX_NAME_LENGTH = 100;

    private PaymentInfo(
        string name,
        string address
        )
    {
        Name = name;
        Address = address;
    }

    public string Name { get; }
    public string Address { get; }

    public static Result<PaymentInfo, string> Create(
        string name,
        string address
        )
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length <= MAX_NAME_LENGTH)
            return "Name should not be empty";

        if (!Regex.IsMatch(address, CREDIT_CARD_REGEX))
            return "Address is not valid";

        return new PaymentInfo(name, address);
    }
}
