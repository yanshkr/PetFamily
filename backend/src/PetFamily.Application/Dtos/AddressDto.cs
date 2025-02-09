namespace PetFamily.Application.Dtos;
public record AddressDto(
    string Country,
    string State,
    string City,
    string Street,
    int ZipCode);
