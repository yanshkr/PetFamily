using Microsoft.AspNetCore.Mvc;
using PetFamily.Api.Controllers.Species.Requests;
using PetFamily.Api.Extensions;
using PetFamily.Application.Features.Species.AddBreed;
using PetFamily.Application.Features.Species.Create;
using PetFamily.Application.Features.Species.Delete;

namespace PetFamily.Api.Controllers.Species;

[ApiController]
[Route("[controller]")]
public class SpeciesController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateSpecieRequest request,
        [FromServices] CreateSpecieHandler createSpecieHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await createSpecieHandler.HandleAsync(command, cancellationToken);

        return result.ToResponse();
    }
    [HttpPost("{id:guid}/breed")]
    public async Task<IActionResult> AddBreed(
        [FromRoute] Guid id,
        [FromBody] AddBreedRequest request,
        [FromServices] AddBreedHandler addBreedHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        var result = await addBreedHandler.HandleAsync(command, cancellationToken);

        return result.ToResponse();
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteSpecieHandler deleteSpecieHandler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteSpecieCommand(id);
        var result = await deleteSpecieHandler.HandleAsync(command, cancellationToken);

        return result.ToResponse();
    }
}