﻿using PetFamily.Application.Features.Commands.Volunteers.DeletePhotosAtPet;

namespace PetFamily.Api.Controllers.Volunteers.Requests;
public record DeletePetPhotosRequest(IEnumerable<string> Photos)
{
    public DeletePetPhotosCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId, petId, Photos);
}