using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;
public record Color
{
    public const int MAX_COLOR_LENGTH = 50;

    private Color(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Color, Error> Create(string color)
    {
        if (string.IsNullOrWhiteSpace(color) || color.Length > MAX_COLOR_LENGTH)
            return Errors.General.ValueIsInvalid("Color");

        return new Color(color);
    }
}
