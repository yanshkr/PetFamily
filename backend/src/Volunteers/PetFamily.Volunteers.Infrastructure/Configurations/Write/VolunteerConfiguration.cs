using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Extensions;
using PetFamily.Volunteers.Domain.Entities;
using PetFamily.Volunteers.Domain.Ids;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Infrastructure.Configurations.Write;
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

        builder.ComplexProperty(
            v => v.Description,
            db =>
            {
                db.Property(d => d.Value)
                    .IsRequired(true)
                    .HasMaxLength(Description.MAX_DESCRIPTION_LENGTH)
                    .HasColumnName("description");
            });

        builder.ComplexProperty(
            v => v.ExperienceYears,
            eb =>
            {
                eb.Property(e => e.Value)
                    .IsRequired(true)
                    .HasColumnName("experience_years");
            });

        builder.ComplexProperty(
            v => v.PhoneNumber,
            pb =>
            {
                pb.Property(p => p.Value)
                    .IsRequired(true)
                    .HasMaxLength(PhoneNumber.MAX_PHONE_NUMBER_LENGTH)
                    .HasColumnName("phone_number");
            });

        builder.HasMany(v => v.Pets)
            .WithOne(p => p.Volunteer)
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(true);

        builder.Property(p => p.PaymentInfos)
            .JsonValueObjectCollectionConversion()
            .IsRequired(true)
            .HasColumnName("payment_infos");

        builder.Property(v => v.SocialMedias)
            .JsonValueObjectCollectionConversion()
            .IsRequired(true)
            .HasColumnName("social_medias");

        builder.Property(v => v.IsDeleted)
            .IsRequired(true)
            .HasColumnName("is_deleted");

        builder.Property(v => v.DeletedAt)
            .SetDefaultDateTimeKind()
            .IsRequired(true)
            .HasColumnName("deleted_at");

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
