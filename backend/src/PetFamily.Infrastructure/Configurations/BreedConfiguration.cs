using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Ids;
using PetFamily.Domain.Species;

namespace PetFamily.Infrastructure.Configurations;
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

        builder.Property(b => b.Name)
            .IsRequired(true)
            .HasMaxLength(PetBreed.MAX_NAME_LENGTH)
            .HasColumnName("name");
    }
}
