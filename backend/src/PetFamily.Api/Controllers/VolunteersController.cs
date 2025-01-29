using Microsoft.AspNetCore.Mvc;
using PetFamily.Api.Extensions;
using PetFamily.Application.Features.Volunteers.CreateVolunteer;
using PetFamily.Application.Features.Volunteers.CreateVolunteer.Contracts;
using PetFamily.Application.Features.Volunteers.UpdateVolunteer;
using PetFamily.Application.Features.Volunteers.UpdateVolunteerMainInfo.Contracts;
using PetFamily.Application.Features.Volunteers.UpdateVolunteerPaymentInfo;
using PetFamily.Application.Features.Volunteers.UpdateVolunteerPaymentInfo.Contracts;
using PetFamily.Application.Features.Volunteers.UpdateVolunteerSocialMedia;
using PetFamily.Application.Features.Volunteers.UpdateVolunteerSocialMedia.Contracts;
using PetFamily.Domain.Volunteers.Ids;

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
    [HttpPut("{id:guid}/social-media")]
    public async Task<IActionResult> UpdateSocialMedia(
        [FromRoute] Guid id,
        [FromServices] UpdateVolunteerSocialMediaHandler updateVolunteerSocialMediaHandler,
        [FromBody] UpdateVolunteerSocialMediaDto dto,
        CancellationToken cancellationToken)
    {
        var request = new UpdateVolunteerSocialMediaRequest(VolunteerId.FromGuid(id), dto);
        var result = await updateVolunteerSocialMediaHandler.HandleAsync(request, cancellationToken);

        return result.ToResponse();
    }
    [HttpPut("{id:guid}/payment-info")]
    public async Task<IActionResult> UpdatePaymentInfo(
        [FromRoute] Guid id,
        [FromServices] UpdateVolunteerPaymentInfoHandler updateVolunteerPaymentInfoHandler,
        [FromBody] UpdateVolunteerPaymentInfoDto dto,
        CancellationToken cancellationToken)
    {
        var request = new UpdateVolunteerPaymentInfoRequest(VolunteerId.FromGuid(id), dto);
        var result = await updateVolunteerPaymentInfoHandler.HandleAsync(request, cancellationToken);

        return result.ToResponse();
    }
}