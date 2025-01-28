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

}