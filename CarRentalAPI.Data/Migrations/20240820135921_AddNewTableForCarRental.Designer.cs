﻿// <auto-generated />
using System;
using CarRentalAPI.Data.CarRentalDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarRentalAPI.Data.Migrations
{
    [DbContext(typeof(CarRentalContext))]
    [Migration("20240820135921_AddNewTableForCarRental")]
    partial class AddNewTableForCarRental
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarRentalAPI.Domain.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("BaseDailyRent")
                        .HasColumnType("float");

                    b.Property<double>("BaseKmPrice")
                        .HasColumnType("float");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("CurrentMileage")
                        .HasColumnType("float");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BaseDailyRent = 200.0,
                            BaseKmPrice = 2.0,
                            Category = "SmallCar",
                            CurrentMileage = 15000.0,
                            RegistrationNumber = "ABC123"
                        },
                        new
                        {
                            Id = 2,
                            BaseDailyRent = 300.0,
                            BaseKmPrice = 2.5,
                            Category = "Kombi",
                            CurrentMileage = 25000.0,
                            RegistrationNumber = "XYZ789"
                        },
                        new
                        {
                            Id = 3,
                            BaseDailyRent = 500.0,
                            BaseKmPrice = 3.0,
                            Category = "Truck",
                            CurrentMileage = 50000.0,
                            RegistrationNumber = "LMN456"
                        });
                });

            modelBuilder.Entity("CarRentalAPI.Domain.Models.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = "123456-7890",
                            Name = "Behrad Hmb"
                        },
                        new
                        {
                            Id = "098765-4321",
                            Name = "Emad Mojaver"
                        });
                });

            modelBuilder.Entity("CarRentalAPI.Domain.Models.Rental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BookingNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double?>("EndMileage")
                        .HasColumnType("float");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime?>("RentalEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RentalStart")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("CarRentalAPI.Domain.Models.Rental", b =>
                {
                    b.HasOne("CarRentalAPI.Domain.Models.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRentalAPI.Domain.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Customer");
                });
#pragma warning restore 612, 618
        }
    }
}
