namespace PetFamily.Application.Dtos;
public class VaccineDto
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public DateTime Date { get; init; }
}