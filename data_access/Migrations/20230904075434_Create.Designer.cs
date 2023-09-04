﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using data_access.Data;

#nullable disable

namespace data_access.Migrations
{
    [DbContext(typeof(OlympiadDBContext))]
    [Migration("20230904075434_Create")]
    partial class Create
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("data_access.Entities.Award", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(56)
                        .HasColumnType("nvarchar(56)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Awards", t =>
                        {
                            t.HasCheckConstraint("Name_check", "[Name] <> ''");
                        });
                });

            modelBuilder.Entity("data_access.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(56)
                        .HasColumnType("nvarchar(56)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Cities", t =>
                        {
                            t.HasCheckConstraint("Name_check", "[Name] <> ''")
                                .HasName("Name_check1");
                        });
                });

            modelBuilder.Entity("data_access.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FlagPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(56)
                        .HasColumnType("nvarchar(56)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Countries", t =>
                        {
                            t.HasCheckConstraint("Name_check", "[Name] <> ''")
                                .HasName("Name_check2");
                        });
                });

            modelBuilder.Entity("data_access.Entities.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(56)
                        .HasColumnType("nvarchar(56)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Gender", t =>
                        {
                            t.HasCheckConstraint("Name_check", "[Name] <> ''")
                                .HasName("Name_check3");
                        });
                });

            modelBuilder.Entity("data_access.Entities.Olympiad_", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("SeasonId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("SeasonId");

                    b.ToTable("Olympiads", t =>
                        {
                            t.HasCheckConstraint("Year", "Year >= 1896");
                        });
                });

            modelBuilder.Entity("data_access.Entities.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(56)
                        .HasColumnType("nvarchar(56)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Seasons", t =>
                        {
                            t.HasCheckConstraint("Name_check", "[Name] <> ''")
                                .HasName("Name_check4");
                        });
                });

            modelBuilder.Entity("data_access.Entities.Sport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(56)
                        .HasColumnType("nvarchar(56)");

                    b.Property<int>("SeasonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("SeasonId");

                    b.ToTable("Sports", t =>
                        {
                            t.HasCheckConstraint("Name_check", "[Name] <> ''")
                                .HasName("Name_check5");
                        });
                });

            modelBuilder.Entity("data_access.Entities.Sportsman", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(56)
                        .HasColumnType("nvarchar(56)");

                    b.Property<string>("PhotoPath")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("SportId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("GenderId");

                    b.HasIndex("SportId");

                    b.ToTable("Sportsmans", t =>
                        {
                            t.HasCheckConstraint("Birthday_check", "Birthday < getdate()");

                            t.HasCheckConstraint("Name_check", "[Name] <> ''")
                                .HasName("Name_check6");
                        });
                });

            modelBuilder.Entity("data_access.Entities.SportsmanAwardOlympiad", b =>
                {
                    b.Property<int>("OlympiadId")
                        .HasColumnType("int");

                    b.Property<int>("SportsmanId")
                        .HasColumnType("int");

                    b.Property<int?>("AwardId")
                        .HasColumnType("int");

                    b.HasKey("OlympiadId", "SportsmanId");

                    b.HasIndex("AwardId");

                    b.HasIndex("SportsmanId");

                    b.ToTable("SportsmanAwardOlympiads");
                });

            modelBuilder.Entity("data_access.Entities.City", b =>
                {
                    b.HasOne("data_access.Entities.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("data_access.Entities.Olympiad_", b =>
                {
                    b.HasOne("data_access.Entities.City", "City")
                        .WithMany("Olympiads")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("data_access.Entities.Season", "Season")
                        .WithMany("Olympiads")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Season");
                });

            modelBuilder.Entity("data_access.Entities.Sport", b =>
                {
                    b.HasOne("data_access.Entities.Season", "Season")
                        .WithMany("Sports")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Season");
                });

            modelBuilder.Entity("data_access.Entities.Sportsman", b =>
                {
                    b.HasOne("data_access.Entities.Country", "Country")
                        .WithMany("Sportsmens")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("data_access.Entities.Gender", "Gender")
                        .WithMany("Sportsmens")
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("data_access.Entities.Sport", "Sport")
                        .WithMany("Sportsmans")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Gender");

                    b.Navigation("Sport");
                });

            modelBuilder.Entity("data_access.Entities.SportsmanAwardOlympiad", b =>
                {
                    b.HasOne("data_access.Entities.Award", "Award")
                        .WithMany("SportsmanOlympiads")
                        .HasForeignKey("AwardId");

                    b.HasOne("data_access.Entities.Olympiad_", "Olympiad")
                        .WithMany("SportsmanAward")
                        .HasForeignKey("OlympiadId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("data_access.Entities.Sportsman", "Sportsman")
                        .WithMany("AwardOlympiads")
                        .HasForeignKey("SportsmanId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Award");

                    b.Navigation("Olympiad");

                    b.Navigation("Sportsman");
                });

            modelBuilder.Entity("data_access.Entities.Award", b =>
                {
                    b.Navigation("SportsmanOlympiads");
                });

            modelBuilder.Entity("data_access.Entities.City", b =>
                {
                    b.Navigation("Olympiads");
                });

            modelBuilder.Entity("data_access.Entities.Country", b =>
                {
                    b.Navigation("Cities");

                    b.Navigation("Sportsmens");
                });

            modelBuilder.Entity("data_access.Entities.Gender", b =>
                {
                    b.Navigation("Sportsmens");
                });

            modelBuilder.Entity("data_access.Entities.Olympiad_", b =>
                {
                    b.Navigation("SportsmanAward");
                });

            modelBuilder.Entity("data_access.Entities.Season", b =>
                {
                    b.Navigation("Olympiads");

                    b.Navigation("Sports");
                });

            modelBuilder.Entity("data_access.Entities.Sport", b =>
                {
                    b.Navigation("Sportsmans");
                });

            modelBuilder.Entity("data_access.Entities.Sportsman", b =>
                {
                    b.Navigation("AwardOlympiads");
                });
#pragma warning restore 612, 618
        }
    }
}