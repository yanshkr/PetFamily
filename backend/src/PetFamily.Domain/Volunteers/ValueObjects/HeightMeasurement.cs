using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers.ValueObjects;
public record HeightMeasurement
{
    private const int MIN_VALUE = 1;
    private const int MAX_VALUE = 100;

    private HeightMeasurement(int centimeters)
    {
        Centimeters = centimeters;
    }

    public int Centimeters { get; }

    public static Result<HeightMeasurement, Error> CreateFromCentimeters(int centimeters)
    {
        if (centimeters is < MIN_VALUE or > MAX_VALUE)
            return Errors.General.ValueIsInvalid("Centimeters", MIN_VALUE, MAX_VALUE);

        return new HeightMeasurement(centimeters);
    }
    public static Result<HeightMeasurement, Error> CreateFromInches(int inches)
    {
        return CreateFromCentimeters((int)Math.Round(inches * 2.54));
    }
    public static Result<HeightMeasurement, Error> CreateFromMetres(decimal metres)
    {
        return CreateFromCentimeters((int)Math.Round(metres * 100));
    }
}
