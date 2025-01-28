using Microsoft.AspNetCore.Mvc;
using PetFamily.Api.Extensions;
using PetFamily.Application.Features.Volunteers.CreateVolunteer;
using Serilog;

namespace PetFamily.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VolunteersController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] CreateVolunteerHandler createVolunteerHandler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken)
    {
       var result = await createVolunteerHandler.HandleAsync(request, cancellationToken);

        return result.ToResponse();
    }

    [HttpPut("{id:guid}/main-info")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromServices] UpdateVolunteerMainInfoHandler updateVolunteerMainInfoHandler,
        [FromBody] UpdateVolunteerDto dto,
        CancellationToken cancellationToken)
    {
        var request = new UpdateVolunteerRequest(VolunteerId.FromGuid(id), dto);
        var result = await updateVolunteerMainInfoHandler.HandleAsync(request, cancellationToken);

        return result.ToResponse();
    }
}