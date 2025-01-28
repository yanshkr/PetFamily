using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Species.Ids;
public class PetBreedId : ComparableValueObject
{
    private PetBreedId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PetBreedId NewPetBreedId() => new(Guid.NewGuid());
    public static PetBreedId FromGuid(Guid guid) => new(guid);
    public static PetBreedId Empty() => new(Guid.Empty);

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator PetBreedId(Guid id) => new(id);

    public static implicit operator Guid(PetBreedId petBreedId)
    {
        ArgumentNullException.ThrowIfNull(petBreedId);
        return petBreedId.Value;
    }
}
