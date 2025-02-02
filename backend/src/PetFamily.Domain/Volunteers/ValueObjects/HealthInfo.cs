using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;
public record HealthInfo
{
    public const int MAX_HEALTHINFO_LENGTH = 500;

    private HealthInfo(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<HealthInfo, Error> Create(string healthInfo)
    {
        if (string.IsNullOrWhiteSpace(healthInfo) || healthInfo.Length > MAX_HEALTHINFO_LENGTH)
            return Errors.General.ValueIsInvalid("HealthInfo");

        return new HealthInfo(healthInfo);
    }
}
