using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Ids;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;

namespace PetFamily.Infrastructure.Configurations;
internal class SpecieConfiguration : IEntityTypeConfiguration<PetSpecie>
{
    public void Configure(EntityTypeBuilder<PetSpecie> builder)
    {
        builder.ToTable("species");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                value => PetSpecieId.FromGuid(value))
            .IsRequired(true)
            .HasColumnName("id");

        builder.Property(s => s.Name)
            .IsRequired(true)
            .HasMaxLength(PetSpecie.MAX_NAME_LENGTH)
            .HasColumnName("name");

        builder.HasMany(s => s.Breeds)
            .WithOne(s => s.Specie)
            .HasForeignKey("specie_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(true);
    }
}
