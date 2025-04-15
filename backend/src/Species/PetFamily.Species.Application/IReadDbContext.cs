using PetFamily.Species.Contracts.Dtos.Breed;
using PetFamily.Species.Contracts.Dtos.Specie;

namespace PetFamily.Species.Application;
public interface IReadDbContext
{
    IQueryable<SpecieDto> Species { get; }
    IQueryable<BreedDto> Breeds { get; }
}
