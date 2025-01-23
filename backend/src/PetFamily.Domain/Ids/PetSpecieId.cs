using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Ids;
public class PetSpecieId : ComparableValueObject
{
    private PetSpecieId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PetSpecieId NewPetSpecieId => new(Guid.NewGuid());
    public static PetSpecieId FromGuid(Guid guid) => new(guid);
    public static PetSpecieId Empty() => new(Guid.Empty);

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}
