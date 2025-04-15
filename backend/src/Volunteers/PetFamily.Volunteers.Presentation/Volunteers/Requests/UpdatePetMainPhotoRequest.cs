﻿using PetFamily.Volunteers.Application.Commands.UpdatePetMainPhoto;

namespace PetFamily.Volunteers.Presentation.Volunteers.Requests;

public record UpdatePetMainPhotoRequest(string FileName)
{
    public UpdatePetMainPhotoCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(volunteerId, petId, FileName);
}