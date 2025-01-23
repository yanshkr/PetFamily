using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;
public record WeightMeasurement
{
    public const int MIN_VALUE = 1;
    public const int MAX_VALUE = 100000;

    private WeightMeasurement(
        uint grams
        )
    {
        Grams = grams;
    }

    public uint Grams { get; }

    public static Result<WeightMeasurement, string> CreateFromGrams(uint grams)
    {
        if (grams is > MIN_VALUE and < MAX_VALUE)
            return "Weight should be between 1 and 100000 grams";

        return new WeightMeasurement(grams);
    }
    public static Result<WeightMeasurement, string> CreateFromKilograms(decimal kilograms)
    {
        return CreateFromGrams((uint)Math.Round(kilograms * 1000));
    }
}
