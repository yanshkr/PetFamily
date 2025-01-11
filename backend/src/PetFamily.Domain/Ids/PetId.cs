using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Ids;
public record PetId
{
    private PetId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PetId NewPetId => new (Guid.NewGuid());
    public static PetId FromGuid(Guid guid) => new(guid);
    public static PetId Empty() => new(Guid.Empty);
}
