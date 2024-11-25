﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Passports.Database;

#nullable disable

namespace Passports.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Passports.Models.Passport", b =>
                {
                    b.Property<short>("Series")
                        .HasColumnType("smallint")
                        .HasColumnName("passp_series");

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("passp_number");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("active");

                    b.HasKey("Series", "Number");

                    b.ToTable("inactivepassports", (string)null);
                });

            modelBuilder.Entity("Passports.Models.PassportHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ActiveEnd")
                        .HasColumnType("date")
                        .HasColumnName("active_end");

                    b.Property<DateTime>("ActiveStart")
                        .HasColumnType("date")
                        .HasColumnName("active_start");

                    b.Property<int>("PassportNumber")
                        .HasColumnType("integer")
                        .HasColumnName("passp_number");

                    b.Property<short>("PassportSeries")
                        .HasColumnType("smallint")
                        .HasColumnName("passp_series");

                    b.HasKey("Id");

                    b.HasIndex("PassportSeries", "PassportNumber");

                    b.ToTable("passporthistory", (string)null);
                });

            modelBuilder.Entity("Passports.Models.UssrPassport", b =>
                {
                    b.Property<string>("Series")
                        .HasColumnType("varchar(9)")
                        .HasColumnName("passp_series");

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("passp_number");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("active");

                    b.HasKey("Series", "Number");

                    b.ToTable("inactiveussrpassports", (string)null);
                });

            modelBuilder.Entity("Passports.Models.UssrPassportHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ActiveEnd")
                        .HasColumnType("date")
                        .HasColumnName("active_end");

                    b.Property<DateTime>("ActiveStart")
                        .HasColumnType("date")
                        .HasColumnName("active_start");

                    b.Property<int>("PassportNumber")
                        .HasColumnType("integer")
                        .HasColumnName("passp_number");

                    b.Property<string>("PassportSeries")
                        .IsRequired()
                        .HasColumnType("varchar(9)")
                        .HasColumnName("passp_series");

                    b.HasKey("Id");

                    b.HasIndex("PassportSeries", "PassportNumber");

                    b.ToTable("ussrpassporthistory", (string)null);
                });

            modelBuilder.Entity("Passports.Models.PassportHistory", b =>
                {
                    b.HasOne("Passports.Models.Passport", "Passport")
                        .WithMany("PassportHistories")
                        .HasForeignKey("PassportSeries", "PassportNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Passport");
                });

            modelBuilder.Entity("Passports.Models.UssrPassportHistory", b =>
                {
                    b.HasOne("Passports.Models.UssrPassport", "UssrPassport")
                        .WithMany("UssrPassportHistories")
                        .HasForeignKey("PassportSeries", "PassportNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UssrPassport");
                });

            modelBuilder.Entity("Passports.Models.Passport", b =>
                {
                    b.Navigation("PassportHistories");
                });

            modelBuilder.Entity("Passports.Models.UssrPassport", b =>
                {
                    b.Navigation("UssrPassportHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
