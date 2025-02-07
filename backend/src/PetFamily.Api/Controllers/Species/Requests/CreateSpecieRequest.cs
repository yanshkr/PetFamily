using PetFamily.Application.Features.Species.Create;

namespace PetFamily.Api.Controllers.Species.Requests;

public record CreateSpecieRequest(string Name)
{
    public CreateSpecieCommand ToCommand() => new(Name);
}
