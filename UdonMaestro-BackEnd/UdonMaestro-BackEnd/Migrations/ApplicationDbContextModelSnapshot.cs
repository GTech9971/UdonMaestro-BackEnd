﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UdonMaestro_BackEnd.Data;

#nullable disable

namespace UdonMaestro_BackEnd.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("UdonMaestro_BackEnd.Data.Model.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Lat")
                        .HasColumnType("numeric(7,4)");

                    b.Property<decimal>("Lon")
                        .HasColumnType("numeric(7,4)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TownId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Lat");

                    b.HasIndex("Lon");

                    b.HasIndex("Name");

                    b.HasIndex("TownId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("UdonMaestro_BackEnd.Data.Model.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("UdonMaestro_BackEnd.Data.Model.Province", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("UdonMaestro_BackEnd.Data.Model.RegularHoliday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("RegularHolidays");
                });

            modelBuilder.Entity("UdonMaestro_BackEnd.Data.Model.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("EnableTakeout")
                        .HasColumnType("boolean");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time without time zone");

                    b.Property<bool>("ExistsCoinParking")
                        .HasColumnType("boolean");

                    b.Property<bool>("ExistsParking")
                        .HasColumnType("boolean");

                    b.Property<string>("Memo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ShopTypeId")
                        .HasColumnType("integer");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time without time zone");

                    b.Property<string>("Tel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("Name");

                    b.HasIndex("ShopTypeId");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("UdonMaestro_BackEnd.Data.Model.ShopRegularHoliday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("RegularHolidayId")
                        .HasColumnType("integer");

                    b.Property<int>("ShopId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RegularHolidayId");

                    b.HasIndex("ShopId");

                    b.ToTable("ShopRegularHoliday");
                });

            modelBuilder.Entity("UdonMaestro_BackEnd.Data.Model.ShopType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("ShopTypes");
                });

            modelBuilder.Entity("UdonMaestro_BackEnd.Data.Model.Town", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CityId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Lat")
                        .HasColumnType("numeric(7,4)");

                    b.Property<decimal>("Lon")
                        .HasColumnType("numeric(7,4)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("Lat");

                    b.HasIndex("Lon");

                    b.HasIndex("Name");

                    b.ToTable("Towns");
                });

            modelBuilder.Entity("UdonMaestro_BackEnd.Data.Model.Address", b =>
                {
                    b.HasOne("UdonMaestro_BackEnd.Data.Model.Town", "Town")
                        .WithMany()
                        .HasForeignKey("TownId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Town");
                });

            modelBuilder.Entity("UdonMaestro_BackEnd.Data.Model.City", b =>
                {
                    b.HasOne("UdonMaestro_BackEnd.Data.Model.Province", "Province")
                        .WithMany("Cities")
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Province");
                });

            modelBuilder.Entity("UdonMaestro_BackEnd.Data.Model.Shop", b =>
                {
                    b.HasOne("UdonMaestro_BackEnd.Data.Model.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UdonMaestro_BackEnd.Data.Model.ShopType", "ShopType")
                        .WithMany()
                        .HasForeignKey("ShopTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("ShopType");
                });

            modelBuilder.Entity("UdonMaestro_BackEnd.Data.Model.ShopRegularHoliday", b =>
                {
                    b.HasOne("UdonMaestro_BackEnd.Data.Model.RegularHoliday", "RegularHoliday")
                        .WithMany()
                        .HasForeignKey("RegularHolidayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UdonMaestro_BackEnd.Data.Model.Shop", "Shop")
                        .WithMany("RegularHolidays")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RegularHoliday");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("UdonMaestro_BackEnd.Data.Model.Town", b =>
                {
                    b.HasOne("UdonMaestro_BackEnd.Data.Model.City", "City")
                        .WithMany("Towns")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("UdonMaestro_BackEnd.Data.Model.City", b =>
                {
                    b.Navigation("Towns");
                });

            modelBuilder.Entity("UdonMaestro_BackEnd.Data.Model.Province", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("UdonMaestro_BackEnd.Data.Model.Shop", b =>
                {
                    b.Navigation("RegularHolidays");
                });
#pragma warning restore 612, 618
        }
    }
}
