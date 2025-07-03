using PetFamily.Species.Application.Queries.GetAllBreedsBySpecieId;

namespace PetFamily.Species.Presentation.Species.Requests;
public record GetAllBreedsRequest(
    string? Name,
    int Page,
    int PageSize)
{
    public GetAllBreedsBySpecieIdQuery ToQuery(Guid specieId)
    {
        return new GetAllBreedsBySpecieIdQuery(
            specieId,
            Name,
            Page,
            PageSize);
    }
};