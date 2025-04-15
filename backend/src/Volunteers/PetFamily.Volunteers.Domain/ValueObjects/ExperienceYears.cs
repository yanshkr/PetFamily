using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ErrorManagement;

namespace PetFamily.Volunteers.Domain.ValueObjects;
public record ExperienceYears
{
    public const int MIN_EXPERIENCE_YEARS = 0;
    public const int MAX_EXPERIENCE_YEARS = 50;

    private ExperienceYears(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static Result<ExperienceYears, Error> Create(int experienceYears)
    {
        if (experienceYears is < MIN_EXPERIENCE_YEARS or > MAX_EXPERIENCE_YEARS)
            return Errors.General.ValueIsInvalid("ExperienceYears");

        return new ExperienceYears(experienceYears);
    }
}
