﻿using PetFamily.Application.Features.Commands.Species.AddBreed;

namespace PetFamily.Api.Controllers.Species.Requests;

public record AddBreedRequest(string Name)
{
    public AddBreedCommand ToCommand(Guid id) => new(id, Name);
}