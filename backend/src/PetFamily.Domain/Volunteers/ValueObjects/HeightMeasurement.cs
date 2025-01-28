using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;
public record HeightMeasurement
{
    private const int MIN_VALUE = 1;
    private const int MAX_VALUE = 100;

    private HeightMeasurement(
        uint centimeters
        )
    {
        Centimeters = centimeters;
    }

    public uint Centimeters { get; }

    public static Result<HeightMeasurement, Error> CreateFromCentimeters(
        uint centimeters
        )
    {
        if (centimeters is > MIN_VALUE and < MAX_VALUE)
            return Errors.General.ValueIsInvalid("Centimeters", MIN_VALUE, MAX_VALUE);

        return new HeightMeasurement(centimeters);
    }
    public static Result<HeightMeasurement, Error> CreateFromInches(
        uint inches
        )
    {
        return CreateFromCentimeters((uint)Math.Round(inches * 2.54));
    }
    public static Result<HeightMeasurement, Error> CreateFromMetres(
        decimal metres
        )
    {
        return CreateFromCentimeters((uint)Math.Round(metres * 100));
    }
}
