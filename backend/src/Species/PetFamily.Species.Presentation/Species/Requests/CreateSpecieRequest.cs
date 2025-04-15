using PetFamily.Species.Application.Commands.CreateSpecie;

namespace PetFamily.Species.Presentation.Species.Requests;

public record CreateSpecieRequest(string Name)
{
    public CreateSpecieCommand ToCommand() => new(Name);
}
