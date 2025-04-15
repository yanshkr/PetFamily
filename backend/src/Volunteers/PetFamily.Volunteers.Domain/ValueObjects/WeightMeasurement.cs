using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ErrorManagement;

namespace PetFamily.Volunteers.Domain.ValueObjects;
public record WeightMeasurement
{
    public const int MIN_VALUE = 1;
    public const int MAX_VALUE = 100000;

    private WeightMeasurement(
        int grams
        )
    {
        Grams = grams;
    }

    public int Grams { get; }

    public static Result<WeightMeasurement, Error> CreateFromGrams(int grams)
    {
        if (grams is < MIN_VALUE or > MAX_VALUE)
            return Errors.General.ValueIsInvalid("Weight", MIN_VALUE, MAX_VALUE);

        return new WeightMeasurement(grams);
    }
    public static Result<WeightMeasurement, Error> CreateFromKilograms(decimal kilograms)
    {
        return CreateFromGrams((int)Math.Round(kilograms * 1000));
    }
}
