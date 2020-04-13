using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OutboundLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 100, nullable: false),
                    OutboundNumber = table.Column<string>(maxLength: 100, nullable: false),
                    OutboundDate = table.Column<DateTime>(maxLength: 50, nullable: false),
                    Note = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboundLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductName = table.Column<string>(maxLength: 100, nullable: false),
                    ProductSpecification = table.Column<string>(maxLength: 50, nullable: false),
                    Amout = table.Column<int>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StorageLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 100, nullable: false),
                    StorageNumber = table.Column<string>(maxLength: 50, nullable: false),
                    StorageDate = table.Column<DateTime>(maxLength: 50, nullable: false),
                    Note = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboundProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OutboundListId = table.Column<Guid>(nullable: false),
                    ProductName = table.Column<string>(maxLength: 100, nullable: false),
                    ProductSpecification = table.Column<string>(maxLength: 50, nullable: false),
                    Amout = table.Column<int>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboundProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutboundProducts_OutboundLists_OutboundListId",
                        column: x => x.OutboundListId,
                        principalTable: "OutboundLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StorageProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StorageListId = table.Column<Guid>(nullable: false),
                    ProductName = table.Column<string>(maxLength: 100, nullable: false),
                    ProductSpecification = table.Column<string>(maxLength: 50, nullable: false),
                    Amout = table.Column<int>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorageProducts_StorageLists_StorageListId",
                        column: x => x.StorageListId,
                        principalTable: "StorageLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OutboundLists",
                columns: new[] { "Id", "Note", "OutboundDate", "OutboundNumber" },
                values: new object[] { new Guid("72457e73-ea34-4e02-b575-8d384e82a481"), "低压加热器管：钢管成集束分布，用蒸汽加热水", new DateTime(2020, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "20200405001" });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "Amout", "ProductName", "ProductSpecification" },
                values: new object[] { new Guid("bbdee09c-089b-4d30-bece-44df59237111"), 5, "电站锅炉", "水管锅炉" });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "Amout", "ProductName", "ProductSpecification" },
                values: new object[] { new Guid("5efc910b-2f45-43df-afae-620d40542800"), 5, "电站锅炉", "混合锅炉" });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "Amout", "ProductName", "ProductSpecification" },
                values: new object[] { new Guid("5efc910b-2f45-43df-afae-620d40542801"), 5, "变压器", "220KV" });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "Amout", "ProductName", "ProductSpecification" },
                values: new object[] { new Guid("5efc910b-2f45-43df-afae-620d40542802"), 5, "变压器", "110KV" });

            migrationBuilder.InsertData(
                table: "StorageLists",
                columns: new[] { "Id", "Note", "StorageDate", "StorageNumber" },
                values: new object[] { new Guid("bbdee09c-089b-4d30-bece-44df5923716c"), "低压加热器管：钢管成集束分布，用蒸汽加热水", new DateTime(2020, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "20200405001" });

            migrationBuilder.InsertData(
                table: "StorageLists",
                columns: new[] { "Id", "Note", "StorageDate", "StorageNumber" },
                values: new object[] { new Guid("6fb600c1-9011-4fd7-9234-881379716440"), null, new DateTime(2020, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "20200405002" });

            migrationBuilder.InsertData(
                table: "StorageLists",
                columns: new[] { "Id", "Note", "StorageDate", "StorageNumber" },
                values: new object[] { new Guid("bbdee09c-089b-4d30-bece-44df5923716e"), "低压加热器管：钢管成集束分布，用蒸汽加热水", new DateTime(2020, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "20200406001" });

            migrationBuilder.InsertData(
                table: "OutboundProducts",
                columns: new[] { "Id", "Amout", "OutboundListId", "ProductName", "ProductSpecification" },
                values: new object[] { new Guid("6fb600c1-9011-4fd7-9234-881379718432"), 1, new Guid("72457e73-ea34-4e02-b575-8d384e82a481"), "电站锅炉", "混合锅炉" });

            migrationBuilder.InsertData(
                table: "StorageProducts",
                columns: new[] { "Id", "Amout", "ProductName", "ProductSpecification", "StorageListId" },
                values: new object[] { new Guid("6fb600c1-9011-4fd7-9234-881379716447"), 3, "电站锅炉", "混合锅炉", new Guid("bbdee09c-089b-4d30-bece-44df5923716c") });

            migrationBuilder.InsertData(
                table: "StorageProducts",
                columns: new[] { "Id", "Amout", "ProductName", "ProductSpecification", "StorageListId" },
                values: new object[] { new Guid("6fb600c1-9011-4fd7-9234-881379716445"), 2, "变压器", "110KV", new Guid("6fb600c1-9011-4fd7-9234-881379716440") });

            migrationBuilder.InsertData(
                table: "StorageProducts",
                columns: new[] { "Id", "Amout", "ProductName", "ProductSpecification", "StorageListId" },
                values: new object[] { new Guid("6fb600c1-9011-4fd7-9234-881379716446"), 2, "变压器", "220KV", new Guid("6fb600c1-9011-4fd7-9234-881379716440") });

            migrationBuilder.InsertData(
                table: "StorageProducts",
                columns: new[] { "Id", "Amout", "ProductName", "ProductSpecification", "StorageListId" },
                values: new object[] { new Guid("6fb600c1-9011-4fd7-9234-881379716444"), 3, "电站锅炉", "水管锅炉", new Guid("bbdee09c-089b-4d30-bece-44df5923716e") });

            migrationBuilder.CreateIndex(
                name: "IX_OutboundProducts_OutboundListId",
                table: "OutboundProducts",
                column: "OutboundListId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageProducts_StorageListId",
                table: "StorageProducts",
                column: "StorageListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutboundProducts");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "StorageProducts");

            migrationBuilder.DropTable(
                name: "OutboundLists");

            migrationBuilder.DropTable(
                name: "StorageLists");
        }
    }
}
