﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MobileApp.Database;

namespace MobileApp.Migrations
{
    [DbContext(typeof(SqliteDbContext))]
    [Migration("20191210181039_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("MobileApp.Database.DTO.Company", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("Name");

                    b.Property<string>("PostalCode");

                    b.Property<string>("VatIdentificationNumber");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("MobileApp.Database.DTO.Receipt", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("BruttoSummary");

                    b.Property<string>("Comment")
                        .HasColumnType("varchar(255)");

                    b.Property<long>("CompanyId");

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<string>("GoogleResponse");

                    b.Property<string>("PicturePath");

                    b.Property<DateTime?>("SaleDate");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("MobileApp.Database.DTO.Receipt", b =>
                {
                    b.HasOne("MobileApp.Database.DTO.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}