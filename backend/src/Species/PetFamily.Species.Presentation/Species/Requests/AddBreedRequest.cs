using PetFamily.Species.Application.Commands.AddBreed;

namespace PetFamily.Species.Presentation.Species.Requests;

public record AddBreedRequest(string Name)
{
    public AddBreedCommand ToCommand(Guid id) => new(id, Name);
}