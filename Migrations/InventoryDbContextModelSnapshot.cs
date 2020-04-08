﻿// <auto-generated />
using System;
using InventoryApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InventoryApi.Migrations
{
    [DbContext(typeof(InventoryDbContext))]
    partial class InventoryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("InventoryApi.Entities.OutboundList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("Note")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<DateTime>("OutboundDate")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("OutboundNumber")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("OutboundLists");

                    b.HasData(
                        new
                        {
                            Id = new Guid("72457e73-ea34-4e02-b575-8d384e82a481"),
                            Note = "低压加热器管：钢管成集束分布，用蒸汽加热水",
                            OutboundDate = new DateTime(2020, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OutboundNumber = "20200405001"
                        });
                });

            modelBuilder.Entity("InventoryApi.Entities.OutboundProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Amout")
                        .HasColumnType("INTEGER")
                        .HasMaxLength(50);

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("ProductSpecification")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<Guid>("StorageListId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("StorageListId");

                    b.ToTable("outboundProducts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6fb600c1-9011-4fd7-9234-881379718432"),
                            Amout = 1,
                            ProductName = "电站锅炉",
                            ProductSpecification = "混合锅炉",
                            StorageListId = new Guid("72457e73-ea34-4e02-b575-8d384e82a481")
                        });
                });

            modelBuilder.Entity("InventoryApi.Entities.Stock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Amout")
                        .HasColumnType("INTEGER")
                        .HasMaxLength(50);

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("ProductSpecification")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Stocks");

                    b.HasData(
                        new
                        {
                            Id = new Guid("bbdee09c-089b-4d30-bece-44df59237111"),
                            Amout = 2,
                            ProductName = "电站锅炉",
                            ProductSpecification = "水管锅炉"
                        },
                        new
                        {
                            Id = new Guid("5efc910b-2f45-43df-afae-620d40542800"),
                            Amout = 2,
                            ProductName = "电站锅炉",
                            ProductSpecification = "混合锅炉"
                        },
                        new
                        {
                            Id = new Guid("5efc910b-2f45-43df-afae-620d40542801"),
                            Amout = 2,
                            ProductName = "变压器",
                            ProductSpecification = "110KV"
                        },
                        new
                        {
                            Id = new Guid("5efc910b-2f45-43df-afae-620d40542802"),
                            Amout = 2,
                            ProductName = "变压器",
                            ProductSpecification = "110KV"
                        });
                });

            modelBuilder.Entity("InventoryApi.Entities.StorageList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("Note")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<DateTime>("StorageDate")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("StorageNumber")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("StorageLists");

                    b.HasData(
                        new
                        {
                            Id = new Guid("bbdee09c-089b-4d30-bece-44df5923716c"),
                            Note = "低压加热器管：钢管成集束分布，用蒸汽加热水",
                            StorageDate = new DateTime(2020, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StorageNumber = "20200405001"
                        },
                        new
                        {
                            Id = new Guid("6fb600c1-9011-4fd7-9234-881379716440"),
                            StorageDate = new DateTime(2020, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StorageNumber = "20200405002"
                        },
                        new
                        {
                            Id = new Guid("bbdee09c-089b-4d30-bece-44df5923716e"),
                            Note = "低压加热器管：钢管成集束分布，用蒸汽加热水",
                            StorageDate = new DateTime(2020, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StorageNumber = "20200406001"
                        });
                });

            modelBuilder.Entity("InventoryApi.Entities.StorageProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Amout")
                        .HasColumnType("INTEGER")
                        .HasMaxLength(50);

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("ProductSpecification")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<Guid>("StorageListId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("StorageListId");

                    b.ToTable("StorageProducts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6fb600c1-9011-4fd7-9234-881379716444"),
                            Amout = 3,
                            ProductName = "电站锅炉",
                            ProductSpecification = "水管锅炉",
                            StorageListId = new Guid("bbdee09c-089b-4d30-bece-44df5923716e")
                        },
                        new
                        {
                            Id = new Guid("6fb600c1-9011-4fd7-9234-881379716445"),
                            Amout = 2,
                            ProductName = "变压器",
                            ProductSpecification = "110KV",
                            StorageListId = new Guid("6fb600c1-9011-4fd7-9234-881379716440")
                        },
                        new
                        {
                            Id = new Guid("6fb600c1-9011-4fd7-9234-881379716446"),
                            Amout = 2,
                            ProductName = "变压器",
                            ProductSpecification = "220KV",
                            StorageListId = new Guid("6fb600c1-9011-4fd7-9234-881379716440")
                        },
                        new
                        {
                            Id = new Guid("6fb600c1-9011-4fd7-9234-881379716447"),
                            Amout = 3,
                            ProductName = "电站锅炉",
                            ProductSpecification = "混合锅炉",
                            StorageListId = new Guid("bbdee09c-089b-4d30-bece-44df5923716c")
                        });
                });

            modelBuilder.Entity("InventoryApi.Entities.OutboundProduct", b =>
                {
                    b.HasOne("InventoryApi.Entities.OutboundList", "OutboundList")
                        .WithMany("OutboundProducts")
                        .HasForeignKey("StorageListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InventoryApi.Entities.StorageProduct", b =>
                {
                    b.HasOne("InventoryApi.Entities.StorageList", "StorageList")
                        .WithMany("StorageProducts")
                        .HasForeignKey("StorageListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
