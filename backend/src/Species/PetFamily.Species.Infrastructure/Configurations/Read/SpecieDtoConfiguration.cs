using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Species.Contracts.Dtos.Specie;

namespace PetFamily.Species.Infrastructure.Configurations.Read;
public class SpecieDtoConfiguration : IEntityTypeConfiguration<SpecieDto>
{
    public void Configure(EntityTypeBuilder<SpecieDto> builder)
    {
        builder.ToTable("species");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .IsRequired(true)
            .HasColumnName("id");

        builder.Property(s => s.Name)
            .IsRequired(true)
            .HasColumnName("name");
    }
}