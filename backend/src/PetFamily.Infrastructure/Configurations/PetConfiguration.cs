using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Entities;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Ids;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using PetFamily.Infrastructure.Configurations.Extensions;

namespace PetFamily.Infrastructure.Configurations;
internal class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => PetId.FromGuid(value))
            .IsRequired(true)
            .HasColumnName("id");

        builder.Property(p => p.Name)
            .IsRequired(true)
            .HasMaxLength(Pet.MAX_NAME_LENGTH)
            .HasColumnName("name");

        builder.Property(p => p.Description)
            .IsRequired(true)
            .HasMaxLength(Pet.MAX_DESCRIPTION_LENGTH)
            .HasColumnName("description");

        builder.Property(p => p.Type)
            .IsRequired(true)
            .HasDefaultValue(PetType.Undefined)
            .HasSentinel(PetType.Undefined)
            .HasConversion<string>()
            .HasColumnName("pet_type");

        builder.HasOne(p => p.Breed)
            .WithMany()
            .HasForeignKey("breed_id")
            .IsRequired(true)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.Specie)
            .WithMany()
            .HasForeignKey("specie_id")
            .IsRequired(true)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(p => p.Color)
            .IsRequired(true)
            .HasMaxLength(Pet.MAX_COLOR_LENGTH)
            .HasColumnName("color");

        builder.Property(p => p.HealthInfo)
            .IsRequired(true)
            .HasMaxLength(Pet.MAX_HEALTH_INFO_LENGTH)
            .HasColumnName("health_info");

        builder.ComplexProperty(
            p => p.Address,
            ab =>
            {
                ab.Property(a => a.Country)
                    .IsRequired(true)
                    .HasMaxLength(Address.MAX_VALUE_LENGTH)
                    .HasColumnName("country");

                ab.Property(a => a.State)
                    .IsRequired(true)
                    .HasMaxLength(Address.MAX_VALUE_LENGTH)
                    .HasColumnName("state");

                ab.Property(a => a.City)
                    .IsRequired(true)
                    .HasMaxLength(Address.MAX_VALUE_LENGTH)
                    .HasColumnName("city");

                ab.Property(a => a.Street)
                    .IsRequired(true)
                    .HasMaxLength(Address.MAX_VALUE_LENGTH)
                    .HasColumnName("street");

                ab.Property(a => a.ZipCode)
                    .IsRequired(true)
                    .HasColumnName("zip_code");
            });

        builder.ComplexProperty(
            p => p.PhoneNumber,
            pb =>
            {
                pb.Property(pn => pn.Value)
                    .IsRequired(true)
                    .HasColumnName("phone_number");
            });

        builder.ComplexProperty(
            p => p.Weight,
            wb =>
            {
                wb.Property(w => w.Grams)
                    .IsRequired(true)
                    .HasColumnName("weight");
            });

        builder.ComplexProperty(
            p => p.Height,
            hb =>
            {
                hb.Property(h => h.Centimeters)
                    .IsRequired(true)
                    .HasColumnName("height");
            });

        builder.Property(p => p.BirthDate)
            .SetDefaultDateTimeKind()
            .IsRequired(true)
            .HasColumnName("birth_date");

        builder.Property(p => p.IsSterilized)
            .IsRequired(true)
            .HasColumnName("sterilized");

        builder.Property(p => p.Status)
            .IsRequired(true)
            .HasDefaultValue(PetStatus.Undefined)
            .HasSentinel(PetStatus.Undefined)
            .HasConversion<string>()
            .HasColumnName("status");

        builder.Property(p => p.Vaccines)
            .JsonValueObjectCollectionConversion()
            .IsRequired(true)
            .HasColumnName("vaccines");

        builder.Property(p => p.PaymentInfos)
            .JsonValueObjectCollectionConversion()
            .IsRequired(true)
            .HasColumnName("payment_infos");

        builder.Property(p => p.UpdatedAt)
            .SetDefaultDateTimeKind()
            .HasDefaultValue(DateTime.MinValue)
            .IsRequired(true)
            .HasColumnName("updated_at");

        builder.Property(p => p.CreatedAt)
            .SetDefaultDateTimeKind()
            .HasDefaultValue(DateTime.MinValue)
            .IsRequired(true)
            .HasColumnName("created_at");
    }
}