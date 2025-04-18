﻿namespace PetFamily.Species.Contracts.Dtos.Breed;
public class BreedDto
{
    public Guid Id { get; init; }
    public Guid SpecieId { get; init; }
    public string Name { get; init; } = null!;
}