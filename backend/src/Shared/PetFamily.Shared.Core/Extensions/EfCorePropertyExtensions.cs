using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace PetFamily.Core.Extensions;
public static class EfCorePropertyExtensions
{
    public static PropertyBuilder<DateTime> SetDefaultDateTimeKind(
        this PropertyBuilder<DateTime> builder,
        DateTimeKind kind = DateTimeKind.Utc)
    {
        return builder.HasConversion(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, kind));
    }
}
