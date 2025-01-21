﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.Ids;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Species;
public class PetBreed : BaseEntity<PetBreedId>
{
    public PetSpecie Specie { get; private set; } = null!;

    private PetBreed() { }

    private PetBreed(
        PetBreedId id,
        string name
        )
    {
        Id = id;
        Name = name;
    }

    public string Name { get; private set; }

    public static Result<PetBreed, string> Create(
        PetBreedId id,
        string name
        )
    {
        if (string.IsNullOrWhiteSpace(name))
            return "Name should not be empty";

        return new PetBreed(id, name);
    }
}
