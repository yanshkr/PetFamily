﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Species;
using PetFamily.Domain.Species.Ids;

namespace PetFamily.Infrastructure.Configurations.Write;
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

        builder.ComplexProperty(
            s => s.Name,
            nb =>
            {
                nb.Property(n => n.Value)
                    .IsRequired(true)
                    .HasMaxLength(Name.MAX_NAME_LENGTH)
                    .HasColumnName("name");
            });

        builder.HasMany(s => s.Breeds)
            .WithOne(s => s.Specie)
            .HasForeignKey("specie_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(true);
    }
}
