using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;
public record PaymentInfo
{
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
        if (string.IsNullOrWhiteSpace(name))
            return "Name should not be empty";

        if (string.IsNullOrWhiteSpace(address))
            return "Address should not be empty";

        return new PaymentInfo(name, address);
    }
}
