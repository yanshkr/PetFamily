﻿using Microsoft.AspNetCore.Mvc;
using PetFamily.Api.Controllers.Volunteers.Requests;
using PetFamily.Api.Extensions;
using PetFamily.Api.Processors;
using PetFamily.Application.Features.Commands.Volunteers.AddPet;
using PetFamily.Application.Features.Commands.Volunteers.CreateVolunteer;
using PetFamily.Application.Features.Commands.Volunteers.DeletePet;
using PetFamily.Application.Features.Commands.Volunteers.DeletePhotosAtPet;
using PetFamily.Application.Features.Commands.Volunteers.DeleteVolunteer;
using PetFamily.Application.Features.Commands.Volunteers.UpdatePetMainInfo;
using PetFamily.Application.Features.Commands.Volunteers.UpdatePetMainPhoto;
using PetFamily.Application.Features.Commands.Volunteers.UpdatePetPosition;
using PetFamily.Application.Features.Commands.Volunteers.UpdatePetStatus;
using PetFamily.Application.Features.Commands.Volunteers.UpdateVolunteerMainInfo;
using PetFamily.Application.Features.Commands.Volunteers.UpdateVolunteerPaymentInfo;
using PetFamily.Application.Features.Commands.Volunteers.UpdateVolunteerSocialMedia;
using PetFamily.Application.Features.Commands.Volunteers.UploadPhotosToPet;
using PetFamily.Application.Features.Queries.Pets.GetAllPetsWithPagination;
using PetFamily.Application.Features.Queries.Pets.GetPetById;
using PetFamily.Application.Features.Queries.Volunteers.GetAllVolunteersWithPagination;
using PetFamily.Application.Features.Queries.Volunteers.GetVolunteerById;

namespace PetFamily.Api.Controllers.Volunteers;

[ApiController]
[Route("[controller]")]
public class VolunteersController : ControllerBase
{
    [HttpGet("{volunteerId:guid}")]
    public async Task<IActionResult> Get(
        [FromRoute] Guid volunteerId,
        [FromServices] GetVolunteerByIdHandler getVolunteerByIdHandler,
        CancellationToken cancellationToken)
    {
        var query = new GetVolunteerByIdQuery(volunteerId);
        var result = await getVolunteerByIdHandler.HandleAsync(query, cancellationToken);

        return result.ToResponse();
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] GetAllVolunteersRequest request,
        [FromServices] GetAllVolunteersWithPaginationHandler getAllVolunteersWithPaginationHandler,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery();
        var result = await getAllVolunteersWithPaginationHandler.HandleAsync(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateVolunteerRequest request,
        [FromServices] CreateVolunteerHandler createVolunteerHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await createVolunteerHandler.HandleAsync(command, cancellationToken);

        return result.ToResponse();
    }
    [HttpPut("{volunteerId:guid}/main-info")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid volunteerId,
        [FromBody] UpdateVolunteerRequest request,
        [FromServices] UpdateVolunteerMainInfoHandler updateVolunteerMainInfoHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(volunteerId);
        var result = await updateVolunteerMainInfoHandler.HandleAsync(command, cancellationToken);

        return result.ToResponse();
    }
    [HttpPut("{volunteerId:guid}/social-media")]
    public async Task<IActionResult> UpdateSocialMedia(
        [FromRoute] Guid volunteerId,
        [FromBody] UpdateVolunteerSocialMediaRequest request,
        [FromServices] UpdateVolunteerSocialMediaHandler updateVolunteerSocialMediaHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(volunteerId);
        var result = await updateVolunteerSocialMediaHandler.HandleAsync(command, cancellationToken);

        return result.ToResponse();
    }
    [HttpPut("{volunteerId:guid}/payment-info")]
    public async Task<IActionResult> UpdatePaymentInfo(
        [FromRoute] Guid volunteerId,
        [FromBody] UpdateVolunteerPaymentInfoRequest request,
        [FromServices] UpdateVolunteerPaymentInfoHandler updateVolunteerPaymentInfoHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(volunteerId);
        var result = await updateVolunteerPaymentInfoHandler.HandleAsync(command, cancellationToken);

        return result.ToResponse();
    }
    [HttpDelete("{volunteerId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid volunteerId,
        [FromQuery] bool IsSoftDelete,
        [FromServices] DeleteVolunteerHandler deleteVolunteerHandler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteVolunteerCommand(volunteerId, IsSoftDelete);
        var result = await deleteVolunteerHandler.HandleAsync(command, cancellationToken);

        return result.ToResponse();
    }
    [HttpGet("pet/{petId:guid}")]
    public async Task<IActionResult> GetPet(
        [FromRoute] Guid petId,
        [FromServices] GetPetByIdHandler getPetByIdHandler,
        CancellationToken cancellationToken)
    {
        var query = new GetPetByIdQuery(petId);
        var result = await getPetByIdHandler.HandleAsync(query, cancellationToken);

        return result.ToResponse();
    }
    [HttpGet("pet")]
    public async Task<IActionResult> GetAllPets(
        [FromQuery] GetAllPetsRequest request,
        [FromServices] GetAllPetsWithPaginationHandler getAllPetsWithPaginationHandler,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery();
        var result = await getAllPetsWithPaginationHandler.HandleAsync(query, cancellationToken);

        return Ok(result);
    }
    [HttpPost("{volunteerId:guid}/pet")]
    public async Task<IActionResult> AddPet(
    [FromRoute] Guid volunteerId,
    [FromBody] AddPetRequest request,
    [FromServices] AddPetHandler addPetHandler,
    CancellationToken cancellationToken)
    {
        var command = request.ToCommand(volunteerId);
        var result = await addPetHandler.HandleAsync(command, cancellationToken);

        return result.ToResponse();
    }
    [HttpPut("{volunteerId:guid}/pet/{petId:guid}/main-info")]
    public async Task<IActionResult> UpdatePetMainInfo(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] UpdatePetMainInfoRequest request,
        [FromServices] UpdatePetMainInfoHandler updatePetMainInfoHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(volunteerId, petId);
        var result = await updatePetMainInfoHandler.HandleAsync(command, cancellationToken);

        return result.ToResponse();
    }
    [HttpPatch("{volunteerId:guid}/pet/{petId:guid}/status")]
    public async Task<IActionResult> UpdatePetStatus(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] UpdatePetStatusRequest request,
        [FromServices] UpdatePetStatusHandler updatePetStatusHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(volunteerId, petId);
        var result = await updatePetStatusHandler.HandleAsync(command, cancellationToken);

        return result.ToResponse();
    }
    [HttpPut("{volunteerId:guid}/pet/{petId:guid}/main-photo")]
    public async Task<IActionResult> UpdatePetMainPhoto(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] UpdatePetMainPhotoRequest request,
        [FromServices] UpdatePetMainPhotoHandler updatePetMainPhotoHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(volunteerId, petId);
        var result = await updatePetMainPhotoHandler.HandleAsync(command, cancellationToken);

        return result.ToResponse();
    }
    [HttpPost("{volunteerId:guid}/pet/{petId:guid}/photos")]
    public async Task<IActionResult> UploadPetPhoto(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromForm] IFormFileCollection files,
        [FromServices] UploadPetPhotosHandler uploadPetPhotoHandler,
        CancellationToken cancellationToken)
    {
        await using var fileProcessor = new FormFileProcessor();
        var fileDtos = fileProcessor.Process(files);

        var command = new UploadPetPhotosCommand(volunteerId, petId, fileDtos);
        var result = await uploadPetPhotoHandler.HandleAsync(command, cancellationToken);

        return result.ToResponse();
    }
    [HttpDelete("{volunteerId:guid}/pet/{petId:guid}/photos")]
    public async Task<IActionResult> DeletePetPhoto(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] DeletePetPhotosRequest request,
        [FromServices] DeletePetPhotosHandler deletePetPhotoHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(volunteerId, petId);
        var result = await deletePetPhotoHandler.HandleAsync(command, cancellationToken);

        return result.ToResponse();
    }
    [HttpPatch("{volunteerId:guid}/pet/{petId:guid}/position")]
    public async Task<IActionResult> UpdatePetPosition(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] UpdatePetPositionRequest request,
        [FromServices] UpdatePetPositionHandler updatePetPositionHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(volunteerId, petId);
        var result = await updatePetPositionHandler.HandleAsync(command, cancellationToken);

        return result.ToResponse();
    }
    [HttpDelete("{volunteerId:guid}/pet/{petId:guid}")]
    public async Task<IActionResult> DeletePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromQuery] bool IsSoftDelete,
        [FromServices] DeletePetHandler deletePetHandler,
        CancellationToken cancellationToken)
    {
        var command = new DeletePetCommand(volunteerId, petId, IsSoftDelete);
        var result = await deletePetHandler.HandleAsync(command, cancellationToken);

        return result.ToResponse();
    }
}