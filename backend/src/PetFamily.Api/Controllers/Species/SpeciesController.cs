using Microsoft.AspNetCore.Mvc;
using PetFamily.Api.Controllers.Species.Requests;
using PetFamily.Api.Extensions;
using PetFamily.Application.Features.Commands.Species.AddBreed;
using PetFamily.Application.Features.Commands.Species.Create;
using PetFamily.Application.Features.Commands.Species.Delete;
using PetFamily.Application.Features.Commands.Species.DeleteBreed;
using PetFamily.Application.Features.Queries.Species.GetAllBreedsBySpecieId;

namespace PetFamily.Api.Controllers.Species;

[ApiController]
[Route("[controller]")]
public class SpeciesController : ControllerBase
{
    [HttpGet("{id:guid}/breeds")]
    public async Task<IActionResult> GetBreeds(
        [FromRoute] Guid id,
        [FromBody] GetAllBreedsRequest request,
        [FromServices] GetAllBreedsBySpecieIdHandler getBreedsHandler,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery(id);
        var result = await getBreedsHandler.HandleAsync(query, cancellationToken);

        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateSpecie(
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
    public async Task<IActionResult> DeleteSpecie(
        [FromRoute] Guid id,
        [FromServices] DeleteSpecieHandler deleteSpecieHandler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteSpecieCommand(id);
        var result = await deleteSpecieHandler.HandleAsync(command, cancellationToken);

        return result.ToResponse();
    }
    [HttpDelete("{specieId:guid}/breed/{breedId:guid}")]
    public async Task<IActionResult> DeleteBreed(
        [FromRoute] Guid specieId,
        [FromRoute] Guid breedId,
        [FromServices] DeleteBreedHandler deleteBreedHandler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteBreedCommand(specieId, breedId);
        var result = await deleteBreedHandler.HandleAsync(command, cancellationToken);

        return result.ToResponse();
    }
}