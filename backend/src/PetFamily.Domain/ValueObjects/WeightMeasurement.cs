namespace PetFamily.Domain.ValueObjects;
public record WeightMeasurement
{
    private WeightMeasurement(
        uint grams
        )
    {
        Grams = grams;
    }

    public uint Grams { get; }

    public static WeightMeasurement CreateFromGrams(uint grams)
    {
        return new WeightMeasurement(grams);
    }
    public static WeightMeasurement CreateFromKilograms(decimal kilograms)
    {
        return new WeightMeasurement((uint)Math.Round(kilograms * 1000));
    }
}
