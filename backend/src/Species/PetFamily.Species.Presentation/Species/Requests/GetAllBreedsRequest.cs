using PetFamily.Species.Application.Queries.GetAllBreedsBySpecieId;

namespace PetFamily.Species.Presentation.Species.Requests;
public record GetAllBreedsRequest(
    string? Name,
    int Page,
    int PageSize)
{
    public GetAllBreedsBySpecieIdQuery ToQuery(Guid SpecieId)
    {
        return new GetAllBreedsBySpecieIdQuery(
            SpecieId,
            Name,
            Page,
            PageSize);
    }
};