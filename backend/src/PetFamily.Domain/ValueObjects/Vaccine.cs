using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;
public class Vaccine
{
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

    public static Result<Vaccine, string> Create(
        string name,
        string description,
        DateTime date
        )
    {
        if (string.IsNullOrWhiteSpace(name))
            return "Name should not be empty";

        if (string.IsNullOrWhiteSpace(description))
            return "Description should not be empty";

        if (date == default)
            return "Date should not be empty";

        return new Vaccine(name, description, date);
    }
}
