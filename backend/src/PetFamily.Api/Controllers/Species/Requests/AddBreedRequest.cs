using PetFamily.Application.Features.Species.AddBreed;
using PetFamily.Domain.Species.Ids;

namespace PetFamily.Api.Controllers.Species.Requests;

public record AddBreedRequest(string Name)
{
    public AddBreedCommand ToCommand(Guid id)
        => new(PetSpecieId.FromGuid(id), Name);
}