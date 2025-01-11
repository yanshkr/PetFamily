using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Ids;
public record VolunteerId
{
    private VolunteerId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static VolunteerId NewVolunteerId => new(Guid.NewGuid());
    public static VolunteerId FromGuid(Guid guid) => new(guid);
    public static VolunteerId Empty() => new(Guid.Empty);
}
