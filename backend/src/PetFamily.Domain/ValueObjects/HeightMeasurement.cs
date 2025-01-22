using CSharpFunctionalExtensions;

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

    public static Result<HeightMeasurement, string> CreateFromCentimeters(
        uint centimeters
        )
    {
        if (centimeters is > MIN_VALUE and < MAX_VALUE)
            return $"Height should be between {MIN_VALUE} and {MAX_VALUE} centimeters";

        return new HeightMeasurement(centimeters);
    }
    public static Result<HeightMeasurement, string> CreateFromInches(
        uint inches
        )
    {
        return CreateFromCentimeters((uint)Math.Round(inches * 2.54));
    }
    public static Result<HeightMeasurement, string> CreateFromMetres(
        decimal metres
        )
    {
        return CreateFromCentimeters((uint)Math.Round(metres * 100));
    }
}
