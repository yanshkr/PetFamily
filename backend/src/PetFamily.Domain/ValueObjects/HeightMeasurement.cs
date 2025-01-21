using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;
public record HeightMeasurement
{
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
        if (centimeters < 0)
            return "Centimeters should not be negative";

        return new HeightMeasurement(centimeters);
    }
    public static Result<HeightMeasurement, string> CreateFromInches(
        uint inches
        )
    {
        if (inches < 0)
            return "Inches should not be negative";

        return new HeightMeasurement((uint)Math.Round(inches * 2.54));
    }
    public static Result<HeightMeasurement, string> CreateFromMetres(
        decimal metres
        )
    {
        if (metres < 0)
            return "Metres should not be negative";

        return new HeightMeasurement((uint)Math.Round(metres * 100));
    }
}
