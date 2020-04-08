using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryApi.Entities;

namespace InventoryApi.Data
{
    public class InventoryDbContext:DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        { }
        //映射数据库
        public DbSet<StorageList> StorageLists { get; set; }
        public DbSet<StorageProduct> StorageProducts { get; set; }
        public DbSet<OutboundList> OutboundLists { get; set; }       
        public DbSet<OutboundProduct> outboundProducts { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        //配置模型
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //配置StorageList
            modelBuilder.Entity<StorageList>()
                .Property(x => x.Id).IsRequired().HasMaxLength(100);
            
            modelBuilder.Entity<StorageList>()
                .Property(x => x.StorageNumber).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<StorageList>()
                .Property(x => x.StorageDate).IsRequired().HasMaxLength(50);   

            modelBuilder.Entity<StorageList>()
                .Property(x => x.Note).HasMaxLength(100);

            //配置StorageProduct
            modelBuilder.Entity<StorageProduct>()
               .Property(x => x.ProductName).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<StorageProduct>()
                .Property(x => x.ProductSpecification).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<StorageProduct>()
               .Property(x => x.Amout).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<StorageProduct>()
                .HasOne(x => x.StorageList)
                .WithMany(x => x.StorageProducts)
                .HasForeignKey(x => x.StorageListId)
                .OnDelete(DeleteBehavior.Cascade);

            //配置OutboundList
            modelBuilder.Entity<OutboundList>()
                .Property(x => x.Id).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<OutboundList>()
                .Property(x => x.OutboundNumber).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<OutboundList>()
                .Property(x => x.OutboundDate).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<OutboundList>()
                .Property(x => x.Note).HasMaxLength(100);

            //配置OutboundProduct
            modelBuilder.Entity<OutboundProduct>()
              .Property(x => x.ProductName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<OutboundProduct>()
                .Property(x => x.ProductSpecification).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<OutboundProduct>()
               .Property(x => x.Amout).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<OutboundProduct>()
                .HasOne(x => x.OutboundList)
                .WithMany(x => x.OutboundProducts)
                .HasForeignKey(x => x.StorageListId)
                .OnDelete(DeleteBehavior.Cascade);

            //配置Stock
            modelBuilder.Entity<Stock>()
                .Property(x => x.ProductName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Stock>()
                .Property(x => x.ProductSpecification).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Stock>()
                .Property(x => x.Amout).IsRequired().HasMaxLength(50);
            //入库单种子
            modelBuilder.Entity<StorageList>().HasData(
                new StorageList
                {
                    Id = Guid.Parse("bbdee09c-089b-4d30-bece-44df5923716c"),
                    StorageNumber = "20200405001",
                    StorageDate = new DateTime(2020, 4, 5),                   
                    Note = "低压加热器管：钢管成集束分布，用蒸汽加热水"
                },
                new StorageList
                {
                    Id = Guid.Parse("6fb600c1-9011-4fd7-9234-881379716440"),
                    StorageNumber = "20200405002",
                    StorageDate = new DateTime(2020, 4, 6)                   
                },
                 new StorageList
                 {
                     Id = Guid.Parse("bbdee09c-089b-4d30-bece-44df5923716e"),
                     StorageNumber = "20200406001",
                     StorageDate = new DateTime(2020, 4, 5),                     
                     Note = "低压加热器管：钢管成集束分布，用蒸汽加热水"
                 }
                );
            //入库产品种子
            modelBuilder.Entity<StorageProduct>().HasData(
                new StorageProduct
                {
                    Id = Guid.Parse("6fb600c1-9011-4fd7-9234-881379716444"),
                    StorageListId = Guid.Parse("bbdee09c-089b-4d30-bece-44df5923716e"),
                    ProductName = "电站锅炉",
                    ProductSpecification = "水管锅炉",
                    Amout = 3,
                },
                new StorageProduct
                {
                    Id = Guid.Parse("6fb600c1-9011-4fd7-9234-881379716445"),
                    StorageListId = Guid.Parse("6fb600c1-9011-4fd7-9234-881379716440"),
                    ProductName = "变压器",
                    ProductSpecification = "110KV",
                    Amout = 2
                },
                new StorageProduct
                {
                    Id = Guid.Parse("6fb600c1-9011-4fd7-9234-881379716446"),
                    StorageListId = Guid.Parse("6fb600c1-9011-4fd7-9234-881379716440"),
                    ProductName = "变压器",
                    ProductSpecification = "220KV",
                    Amout = 2
                },
                new StorageProduct
                {
                    Id = Guid.Parse("6fb600c1-9011-4fd7-9234-881379716447"),
                    StorageListId = Guid.Parse("bbdee09c-089b-4d30-bece-44df5923716c"),
                    ProductName = "电站锅炉",
                    ProductSpecification = "混合锅炉",
                    Amout = 3,
                }
                );

            //出库单种子
            modelBuilder.Entity<OutboundList>().HasData(
                new OutboundList
                {
                    Id = Guid.Parse("72457e73-ea34-4e02-b575-8d384e82a481"),
                    OutboundNumber = "20200405001",
                    OutboundDate = new DateTime(2020, 4, 5),
                    
                    Note = "低压加热器管：钢管成集束分布，用蒸汽加热水"
                });

            //出库单内产品种子
            modelBuilder.Entity<OutboundProduct>().HasData(
                new OutboundProduct
                {
                    Id = Guid.Parse("6fb600c1-9011-4fd7-9234-881379718432"),
                    StorageListId = Guid.Parse("72457e73-ea34-4e02-b575-8d384e82a481"),
                    ProductName = "电站锅炉",
                    ProductSpecification = "混合锅炉",
                    Amout = 1,
                });

            //库存种子
            modelBuilder.Entity<Stock>().HasData(
                new Stock
                {
                    Id = Guid.Parse("bbdee09c-089b-4d30-bece-44df59237111"),
                    ProductName = "电站锅炉",
                    ProductSpecification = "水管锅炉",
                    Amout = 2
                },
                new Stock
                {
                    Id = Guid.Parse("5efc910b-2f45-43df-afae-620d40542800"),
                    ProductName = "电站锅炉",
                    ProductSpecification = "混合锅炉",
                    Amout = 2,
                },
                new Stock
                {
                    Id = Guid.Parse("5efc910b-2f45-43df-afae-620d40542801"),
                    ProductName = "变压器",
                    ProductSpecification = "110KV",
                    Amout = 2,
                },
                new Stock
                {
                    Id = Guid.Parse("5efc910b-2f45-43df-afae-620d40542802"),
                    ProductName = "变压器",
                    ProductSpecification = "110KV",
                    Amout = 2,
                });

        }
    }
}
