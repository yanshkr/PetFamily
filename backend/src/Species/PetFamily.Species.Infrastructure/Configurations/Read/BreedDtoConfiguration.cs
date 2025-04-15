using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Species.Contracts.Dtos.Breed;

namespace PetFamily.Species.Infrastructure.Configurations.Read;
public class BreedDtoConfiguration : IEntityTypeConfiguration<BreedDto>
{
    public void Configure(EntityTypeBuilder<BreedDto> builder)
    {
        builder.ToTable("breeds");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .IsRequired(true)
            .HasColumnName("id");

        builder.Property(b => b.SpecieId)
            .IsRequired(true)
            .HasColumnName("specie_id");

        builder.Property(b => b.Name)
            .IsRequired(true)
            .HasColumnName("name");
    }
}
