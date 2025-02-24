namespace PetFamily.Application.Dtos;
public class AddressDto
{
    public string Country { get; init; } = null!;
    public string State { get; init; } = null!;
    public string City { get; init; } = null!;
    public string Street { get; init; } = null!;
    public int ZipCode { get; init; }
}