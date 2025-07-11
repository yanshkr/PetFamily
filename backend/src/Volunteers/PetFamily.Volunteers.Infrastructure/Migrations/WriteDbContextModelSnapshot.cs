﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetFamily.Volunteers.Infrastructure.DbContexts;

#nullable disable

namespace PetFamily.Volunteers.Infrastructure.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    partial class WriteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("volunteers")
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetFamily.Volunteers.Domain.Entities.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birth_date");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc))
                        .HasColumnName("created_at");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSterilized")
                        .HasColumnType("boolean")
                        .HasColumnName("is_sterilized");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("Undefined")
                        .HasColumnName("status");

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("Undefined")
                        .HasColumnName("type");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc))
                        .HasColumnName("updated_at");

                    b.Property<Guid>("volunteer_id")
                        .HasColumnType("uuid");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "PetFamily.Volunteers.Domain.Entities.Pet.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("character varying(200)")
                                .HasColumnName("city");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("character varying(200)")
                                .HasColumnName("country");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("character varying(200)")
                                .HasColumnName("state");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("character varying(200)")
                                .HasColumnName("street");

                            b1.Property<int>("ZipCode")
                                .HasColumnType("integer")
                                .HasColumnName("zip_code");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Color", "PetFamily.Volunteers.Domain.Entities.Pet.Color#Color", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("color");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Description", "PetFamily.Volunteers.Domain.Entities.Pet.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .ValueGeneratedOnUpdateSometimes()
                                .HasMaxLength(500)
                                .HasColumnType("character varying(500)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("HealthInfo", "PetFamily.Volunteers.Domain.Entities.Pet.HealthInfo#HealthInfo", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("character varying(500)")
                                .HasColumnName("health_info");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Height", "PetFamily.Volunteers.Domain.Entities.Pet.Height#HeightMeasurement", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Centimeters")
                                .HasColumnType("integer")
                                .HasColumnName("height");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("MainPhoto", "PetFamily.Volunteers.Domain.Entities.Pet.MainPhoto#Photo", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FileName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("main_photo");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetFamily.Volunteers.Domain.Entities.Pet.Name#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .ValueGeneratedOnUpdateSometimes()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("name");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PetClassification", "PetFamily.Volunteers.Domain.Entities.Pet.PetClassification#PetClassification", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<Guid>("BreedId")
                                .HasColumnType("uuid")
                                .HasColumnName("breed_id");

                            b1.Property<Guid>("SpecieId")
                                .HasColumnType("uuid")
                                .HasColumnName("specie_id");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PetPosition", "PetFamily.Volunteers.Domain.Entities.Pet.PetPosition#PetPosition", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("position");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumber", "PetFamily.Volunteers.Domain.Entities.Pet.PhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("phone_number");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Weight", "PetFamily.Volunteers.Domain.Entities.Pet.Weight#WeightMeasurement", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Grams")
                                .HasColumnType("integer")
                                .HasColumnName("weight");
                        });

                    b.HasKey("Id");

                    b.HasIndex("volunteer_id");

                    b.ToTable("pets", "volunteers");
                });

            modelBuilder.Entity("PetFamily.Volunteers.Domain.Entities.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "PetFamily.Volunteers.Domain.Entities.Volunteer.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("character varying(500)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Email", "PetFamily.Volunteers.Domain.Entities.Volunteer.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("email");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("ExperienceYears", "PetFamily.Volunteers.Domain.Entities.Volunteer.ExperienceYears#ExperienceYears", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("experience_years");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "PetFamily.Volunteers.Domain.Entities.Volunteer.FullName#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("first_name");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("last_name");

                            b1.Property<string>("MiddleName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("middle_name");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumber", "PetFamily.Volunteers.Domain.Entities.Volunteer.PhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(15)
                                .HasColumnType("character varying(15)")
                                .HasColumnName("phone_number");
                        });

                    b.HasKey("Id");

                    b.ToTable("volunteers", "volunteers");
                });

            modelBuilder.Entity("PetFamily.Volunteers.Domain.Entities.Pet", b =>
                {
                    b.HasOne("PetFamily.Volunteers.Domain.Entities.Volunteer", "Volunteer")
                        .WithMany("Pets")
                        .HasForeignKey("volunteer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("PetFamily.Volunteers.Domain.ValueObjects.PaymentInfo", "PaymentInfos", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid");

                            b1.Property<int>("__synthesizedOrdinal")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("address");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("text")
                                .HasColumnName("name");

                            b1.HasKey("PetId", "__synthesizedOrdinal");

                            b1.ToTable("pets", "volunteers");

                            b1.ToJson("payment_infos");

                            b1.WithOwner()
                                .HasForeignKey("PetId");
                        });

                    b.OwnsMany("PetFamily.Volunteers.Domain.ValueObjects.Photo", "Photos", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid");

                            b1.Property<int>("__synthesizedOrdinal")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            b1.Property<string>("FileName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("filename");

                            b1.HasKey("PetId", "__synthesizedOrdinal");

                            b1.ToTable("pets", "volunteers");

                            b1.ToJson("photos");

                            b1.WithOwner()
                                .HasForeignKey("PetId");
                        });

                    b.OwnsMany("PetFamily.Volunteers.Domain.ValueObjects.Vaccine", "Vaccines", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid");

                            b1.Property<int>("__synthesizedOrdinal")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("date");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("text")
                                .HasColumnName("description");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("text")
                                .HasColumnName("name");

                            b1.HasKey("PetId", "__synthesizedOrdinal");

                            b1.ToTable("pets", "volunteers");

                            b1.ToJson("vaccines");

                            b1.WithOwner()
                                .HasForeignKey("PetId");
                        });

                    b.Navigation("PaymentInfos");

                    b.Navigation("Photos");

                    b.Navigation("Vaccines");

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("PetFamily.Volunteers.Domain.Entities.Volunteer", b =>
                {
                    b.OwnsMany("PetFamily.Volunteers.Domain.ValueObjects.PaymentInfo", "PaymentInfos", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid");

                            b1.Property<int>("__synthesizedOrdinal")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("address");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("text")
                                .HasColumnName("name");

                            b1.HasKey("VolunteerId", "__synthesizedOrdinal");

                            b1.ToTable("volunteers", "volunteers");

                            b1.ToJson("payment_infos");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId");
                        });

                    b.OwnsMany("PetFamily.Volunteers.Domain.ValueObjects.SocialMedia", "SocialMedias", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid");

                            b1.Property<int>("__synthesizedOrdinal")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("text")
                                .HasColumnName("name");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("url");

                            b1.HasKey("VolunteerId", "__synthesizedOrdinal");

                            b1.ToTable("volunteers", "volunteers");

                            b1.ToJson("social_medias");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId");
                        });

                    b.Navigation("PaymentInfos");

                    b.Navigation("SocialMedias");
                });

            modelBuilder.Entity("PetFamily.Volunteers.Domain.Entities.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
