using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Ids;
public record PetBreedId
{
    private PetBreedId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PetBreedId NewPetBreedId => new(Guid.NewGuid());
    public static PetBreedId FromGuid(Guid guid) => new(guid);
    public static PetBreedId Empty() => new(Guid.Empty);
}
