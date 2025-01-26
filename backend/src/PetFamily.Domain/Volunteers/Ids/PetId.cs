using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Volunteers.Ids;
public class PetId : ComparableValueObject
{
    private PetId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PetId NewPetId() => new(Guid.NewGuid());
    public static PetId FromGuid(Guid guid) => new(guid);
    public static PetId Empty() => new(Guid.Empty);

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator PetId(Guid id) => new(id);

    public static implicit operator Guid(PetId petId)
    {
        ArgumentNullException.ThrowIfNull(petId);
        return petId.Value;
    }
}
