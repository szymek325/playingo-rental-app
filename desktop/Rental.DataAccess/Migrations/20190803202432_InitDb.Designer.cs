﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rental.DataAccess.Context;

namespace Rental.DataAccess.Migrations
{
    [DbContext(typeof(RentalContext))]
    [Migration("20190803202432_InitDb")]
    partial class InitDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("r")
                .HasAnnotation("ProductVersion", "3.0.0-preview7.19362.6");

            modelBuilder.Entity("Rental.Core.Entities.BoardGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2019, 8, 3, 20, 24, 32, 726, DateTimeKind.Utc).AddTicks(3741));

                    b.Property<string>("Name");

                    b.Property<float>("PricePerDay");

                    b.Property<float>("SuggestedDeposit");

                    b.HasKey("Id");

                    b.ToTable("BoardGames");
                });

            modelBuilder.Entity("Rental.Core.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContactNumber");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2019, 8, 3, 20, 24, 32, 728, DateTimeKind.Utc).AddTicks(8211));

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "mat"
                        },
                        new
                        {
                            Id = 2,
                            CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "test2"
                        });
                });

            modelBuilder.Entity("Rental.Core.Entities.GameRental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BoardGameId");

                    b.Property<float>("ChargedDeposit");

                    b.Property<int>("ClientId");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2019, 8, 3, 20, 24, 32, 728, DateTimeKind.Utc).AddTicks(8403));

                    b.Property<float>("PaidMoney");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("BoardGameId");

                    b.HasIndex("ClientId");

                    b.ToTable("GameRentals");
                });

            modelBuilder.Entity("Rental.Core.Entities.GameRental", b =>
                {
                    b.HasOne("Rental.Core.Entities.BoardGame", "BoardGame")
                        .WithMany("GameRentals")
                        .HasForeignKey("BoardGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rental.Core.Entities.Client", "Client")
                        .WithMany("GameRentals")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
