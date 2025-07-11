﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Contracts.Dtos.Pet;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Infrastructure.Configurations.Read;
public class PetDtoConfiguration : IEntityTypeConfiguration<PetDto>
{
    public void Configure(EntityTypeBuilder<PetDto> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Name);

        builder.Property(p => p.Id)
            .IsRequired(true)
            .HasColumnName("id");

        builder.Property(p => p.VolunteerId)
            .IsRequired(true)
            .HasColumnName("volunteer_id");

        builder.Property(p => p.Name)
            .IsRequired(true)
            .HasMaxLength(Name.MAX_NAME_LENGTH)
            .HasColumnName("name");

        builder.Property(p => p.Description)
            .IsRequired(true)
            .HasMaxLength(Description.MAX_DESCRIPTION_LENGTH)
            .HasColumnName("description");

        builder.Property(p => p.Type)
            .IsRequired(true)
            .HasConversion<string>()
            .HasColumnName("type");

        builder.Property(p => p.BreedId)
            .IsRequired(true)
            .HasColumnName("breed_id");

        builder.Property(p => p.SpecieId)
            .IsRequired(true)
            .HasColumnName("specie_id");

        builder.Property(p => p.Color)
            .IsRequired(true)
            .HasMaxLength(Color.MAX_COLOR_LENGTH)
            .HasColumnName("color");

        builder.Property(p => p.HealthInfo)
            .IsRequired(true)
            .HasMaxLength(HealthInfo.MAX_HEALTHINFO_LENGTH)
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

        builder.Property(p => p.PhoneNumber)
            .IsRequired(true)
            .HasMaxLength(PhoneNumber.MAX_PHONE_NUMBER_LENGTH)
            .HasColumnName("phone_number");

        builder.Property(p => p.Weight)
            .IsRequired(true)
            .HasColumnName("weight");

        builder.Property(p => p.Height)
            .IsRequired(true)
            .HasColumnName("height");

        builder.Property(p => p.BirthDate)
            .IsRequired(true)
            .HasColumnName("birth_date");

        builder.Property(p => p.IsSterilized)
            .IsRequired(true)
            .HasColumnName("is_sterilized");

        builder.Property(p => p.PetPosition)
            .IsRequired(true)
            .HasColumnName("position");

        builder.Property(p => p.MainPhoto)
            .IsRequired(true)
            .HasColumnName("main_photo");

        builder.OwnsMany(p => p.Vaccines, vb =>
        {
            vb.ToJson("vaccines");

            vb.Property(v => v.Name)
                .IsRequired(true)
                .HasColumnName("name");

            vb.Property(v => v.Description)
                .IsRequired(true)
                .HasColumnName("description");

            vb.Property(v => v.Date)
                .IsRequired(true)
                .HasColumnName("date");
        });

        builder.OwnsMany(p => p.PaymentInfos, pb =>
        {
            pb.ToJson("payment_infos");

            pb.Property(v => v.Name)
                .IsRequired(true)
                .HasColumnName("name");

            pb.Property(v => v.Address)
                .IsRequired(true)
                .HasColumnName("address");
        });

        builder.OwnsMany(p => p.Photos, pb =>
        {
            pb.ToJson("photos");

            pb.Property(v => v.FileName)
                .IsRequired(true)
                .HasColumnName("filename");
        });

        builder.Property(p => p.Status)
            .IsRequired(true)
            .HasConversion<string>()
            .HasColumnName("status");
    }
}
