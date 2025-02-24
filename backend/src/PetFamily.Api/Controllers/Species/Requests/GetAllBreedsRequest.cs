using PetFamily.Application.Features.Queries.Species.GetAllBreedsBySpecieId;

namespace PetFamily.Api.Controllers.Species.Requests;
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