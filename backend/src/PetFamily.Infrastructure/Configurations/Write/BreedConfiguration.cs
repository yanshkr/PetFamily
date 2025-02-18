using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Species.Entities;
using PetFamily.Domain.Species.Ids;

namespace PetFamily.Infrastructure.Configurations.Write;
internal class BreedConfiguration : IEntityTypeConfiguration<PetBreed>
{
    public void Configure(EntityTypeBuilder<PetBreed> builder)
    {
        builder.ToTable("breeds");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasConversion(
                id => id.Value,
                value => PetBreedId.FromGuid(value))
            .IsRequired(true)
            .HasColumnName("id");

        builder.ComplexProperty(
            b => b.Name,
            nb =>
            {
                nb.Property(n => n.Value)
                    .IsRequired(true)
                    .HasMaxLength(Name.MAX_NAME_LENGTH)
                    .HasColumnName("name");
            });
    }
}
