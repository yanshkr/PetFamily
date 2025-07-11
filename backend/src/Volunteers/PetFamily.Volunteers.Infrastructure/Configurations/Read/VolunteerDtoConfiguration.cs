﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Extensions;
using PetFamily.Volunteers.Contracts.Dtos.Volunteer;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Infrastructure.Configurations.Read;
public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerDto>
{
    public void Configure(EntityTypeBuilder<VolunteerDto> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .IsRequired(true)
            .HasColumnName("id");

        builder.Property(v => v.FirstName)
            .IsRequired(true)
            .HasMaxLength(FullName.MAX_VALUE_LENGTH)
            .HasColumnName("first_name");

        builder.Property(v => v.MiddleName)
            .IsRequired(true)
            .HasMaxLength(FullName.MAX_VALUE_LENGTH)
            .HasColumnName("middle_name");

        builder.Property(v => v.LastName)
            .IsRequired(true)
            .HasMaxLength(FullName.MAX_VALUE_LENGTH)
            .HasColumnName("last_name");

        builder.Property(v => v.Email)
            .IsRequired(true)
            .HasMaxLength(Email.MAX_EMAIL_LENGTH)
            .HasColumnName("email");

        builder.Property(v => v.PhoneNumber)
            .IsRequired(true)
            .HasMaxLength(PhoneNumber.MAX_PHONE_NUMBER_LENGTH)
            .HasColumnName("phone_number");

        builder.Property(v => v.Description)
            .IsRequired(true)
            .HasMaxLength(Description.MAX_DESCRIPTION_LENGTH)
            .HasColumnName("description");

        builder.Property(v => v.ExperienceYears)
            .IsRequired(true)
            .HasColumnName("experience_years");

        builder.OwnsMany(v => v.PaymentInfos, pb =>
        {
            pb.ToJson("payment_infos");

            pb.Property(p => p.Name)
                .IsRequired(true)
                .HasColumnName("name");

            pb.Property(p => p.Address)
                .IsRequired(true)
                .HasColumnName("address");
        });

        builder.OwnsMany(v => v.SocialMedias, smb =>
        {
            smb.ToJson("social_medias");

            smb.Property(p => p.Url)
                .IsRequired(true)
                .HasColumnName("url");

            smb.Property(p => p.Name)
                .IsRequired(true)
                .HasColumnName("name");
        });
    }
}
