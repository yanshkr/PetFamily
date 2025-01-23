using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Ids;
public class PetBreedId : ComparableValueObject
{
    private PetBreedId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PetBreedId NewPetBreedId => new(Guid.NewGuid());
    public static PetBreedId FromGuid(Guid guid) => new(guid);
    public static PetBreedId Empty() => new(Guid.Empty);

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}
