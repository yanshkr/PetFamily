using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Ids;
public class PetId : ComparableValueObject
{
    private PetId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PetId NewPetId => new(Guid.NewGuid());
    public static PetId FromGuid(Guid guid) => new(guid);
    public static PetId Empty() => new(Guid.Empty);

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}
