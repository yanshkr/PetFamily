using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Volunteers.Ids;
public class VolunteerId : ComparableValueObject
{
    private VolunteerId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static VolunteerId NewVolunteerId() => new(Guid.NewGuid());
    public static VolunteerId FromGuid(Guid guid) => new(guid);
    public static VolunteerId Empty() => new(Guid.Empty);

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator VolunteerId(Guid id) => new(id);

    public static implicit operator Guid(VolunteerId volunteerId)
    {
        ArgumentNullException.ThrowIfNull(volunteerId);
        return volunteerId.Value;
    }
}
