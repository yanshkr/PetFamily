using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;
public class Vaccine
{
    public const int MAX_NAME_LENGTH = 100;
    public const int MAX_DESCRIPTION_LENGTH = 1000;

    private Vaccine(
        string name,
        string description,
        DateTime date
        )
    {
        Name = name;
        Description = description;
        Date = date;
    }

    public string Name { get; }
    public string Description { get; }
    public DateTime Date { get; }

    public static Result<Vaccine, Error> Create(
        string name,
        string description,
        DateTime date
        )
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH)
            return Errors.General.ValueIsInvalid("Name");

        if (string.IsNullOrWhiteSpace(description) || description.Length > MAX_DESCRIPTION_LENGTH)
            return Errors.General.ValueIsInvalid("Description");

        if (date == default)
            return Errors.General.ValueIsInvalid("Date");

        return new Vaccine(name, description, date);
    }
}
