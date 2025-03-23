using PetFamily.Domain.Volunteers.Enums;

namespace PetFamily.Application.Dtos;
public class PetDto
{
    public Guid Id { get; init; }
    public Guid VolunteerId { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public PetType Type { get; init; }
    public Guid BreedId { get; init; }
    public Guid SpecieId { get; init; }
    public string Color { get; init; } = null!;
    public string HealthInfo { get; init; } = null!;
    public AddressDto Address { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
    public int Weight { get; init; }
    public int Height { get; init; }
    public DateTime BirthDate { get; init; }
    public bool IsSterilized { get; init; }
    public int PetPosition { get; init; }
    public string MainPhoto { get; init; } = null!;
    public IReadOnlyList<VaccineDto> Vaccines { get; init; } = [];
    public IReadOnlyList<PaymentInfoDto> PaymentInfos { get; init; } = [];
    public IReadOnlyList<PhotoDto> Photos { get; init; } = [];
    public PetStatus Status { get; init; }
}