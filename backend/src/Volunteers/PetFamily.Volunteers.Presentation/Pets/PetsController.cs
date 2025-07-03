using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Framework.Extensions;
using PetFamily.Volunteers.Application.Queries.GetAllPetsWithPagination;
using PetFamily.Volunteers.Application.Queries.GetPetById;
using PetFamily.Volunteers.Presentation.Pets.Requests;

namespace PetFamily.Volunteers.Presentation.Pets;
[ApiController]
[Authorize]
[Route("[controller]")]
internal class PetsController : ControllerBase
{
    [HttpGet("{petId:guid}")]
    public async Task<IActionResult> GetPet(
    [FromRoute] Guid petId,
    [FromServices] GetPetByIdHandler getPetByIdHandler,
    CancellationToken cancellationToken)
    {
        var query = new GetPetByIdQuery(petId);
        var result = await getPetByIdHandler.HandleAsync(query, cancellationToken);

        return result.ToResponse();
    }
    [HttpGet]
    public async Task<IActionResult> GetAllPets(
        [FromQuery] GetAllPetsRequest request,
        [FromServices] GetAllPetsWithPaginationHandler getAllPetsWithPaginationHandler,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery();
        var result = await getAllPetsWithPaginationHandler.HandleAsync(query, cancellationToken);

        return Ok(result);
    }
}
