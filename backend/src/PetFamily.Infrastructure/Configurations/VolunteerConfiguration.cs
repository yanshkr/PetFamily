using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Entities;
using PetFamily.Domain.Ids;
using PetFamily.Domain.ValueObjects;
using PetFamily.Infrastructure.Configurations.Extensions;

namespace PetFamily.Infrastructure.Configurations;
internal class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerId.FromGuid(value))
            .IsRequired(true)
            .HasColumnName("id");

        builder.ComplexProperty(
            v => v.FullName,
            fb =>
            {
                fb.Property(f => f.FirstName)
                    .IsRequired(true)
                    .HasMaxLength(FullName.MAX_VALUE_LENGTH)
                    .HasColumnName("first_name");

                fb.Property(f => f.MiddleName)
                .IsRequired(true)
                    .HasMaxLength(FullName.MAX_VALUE_LENGTH)
                    .HasColumnName("middle_name");

                fb.Property(f => f.LastName)
                    .IsRequired(true)
                    .HasMaxLength(FullName.MAX_VALUE_LENGTH)
                    .HasColumnName("last_name");
            });

        builder.ComplexProperty(
            v => v.Email,
            pb =>
            {
                pb.Property(p => p.Value)
                    .IsRequired(true)
                    .HasMaxLength(Email.MAX_EMAIL_LENGTH)
                    .HasColumnName("email");
            });

        builder.Property(v => v.Description)
            .IsRequired(true)
            .HasMaxLength(Volunteer.MAX_DESCRIPTION_LENGTH)
            .HasColumnName("description");

        builder.Property(v => v.ExperienceYears)
            .IsRequired(true)
            .HasColumnName("experience_years");

        builder.ComplexProperty(
            v => v.PhoneNumber,
            pb =>
            {
                pb.Property(p => p.Value)
                    .IsRequired(true)
                    .HasMaxLength(15)
                    .HasColumnName("phone_number");
            });

        builder.HasMany(v => v.Pets)
            .WithOne(p => p.Volunteer)
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(true);

        builder.Property(v => v.PaymentInfos)
            .JsonValueObjectCollectionConversion()
            .IsRequired(true)
            .HasColumnName("payment_infos");

        builder.Property(v => v.SocialMedias)
            .JsonValueObjectCollectionConversion()
            .IsRequired(true)
            .HasColumnName("social_medias");

        builder.Property(v => v.UpdatedAt)
            .SetDefaultDateTimeKind()
            .IsRequired(true)
            .HasColumnName("updated_at");

        builder.Property(v => v.CreatedAt)
            .SetDefaultDateTimeKind()
            .IsRequired(true)
            .HasColumnName("created_at");
    }
}
