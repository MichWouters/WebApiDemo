using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiDemo.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naam = table.Column<string>(type: "text", nullable: false),
                    Voornaam = table.Column<string>(type: "text", nullable: false),
                    AangemaaktDatum = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Laptops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Merk = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Processor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RamInGB = table.Column<int>(type: "integer", nullable: false),
                    Prijs = table.Column<double>(type: "double precision", precision: 18, scale: 2, nullable: false),
                    GPU = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laptops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naam = table.Column<string>(type: "text", nullable: false),
                    Beschrijving = table.Column<string>(type: "text", nullable: true),
                    Prijs = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bestelling",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KlantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestelling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bestelling_Klant_KlantId",
                        column: x => x.KlantId,
                        principalTable: "Klant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderLijn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Aantal = table.Column<double>(type: "double precision", nullable: false),
                    BestellingId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLijn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLijn_Bestelling_BestellingId",
                        column: x => x.BestellingId,
                        principalTable: "Bestelling",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLijn_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Klant",
                columns: new[] { "Id", "AangemaaktDatum", "Naam", "Voornaam" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 10, 15, 12, 0, 0, 0, DateTimeKind.Utc), "Van Der Neffe", "Leon" },
                    { 2, new DateTime(2014, 10, 15, 12, 0, 0, 0, DateTimeKind.Utc), "Van De Kasseinen", "Firmin" },
                    { 3, new DateTime(2023, 10, 15, 12, 0, 0, 0, DateTimeKind.Utc), "Kiekeboe", "Marcel" }
                });

            migrationBuilder.InsertData(
                table: "Laptops",
                columns: new[] { "Id", "GPU", "Merk", "Prijs", "Processor", "RamInGB" },
                values: new object[,]
                {
                    { 1, "Apple M3 Pro 18-core", "Apple", 2499.0, "M3 Pro", 18 },
                    { 2, "NVIDIA RTX 4060", "Dell", 1299.99, "Intel Core i7-13700H", 16 },
                    { 3, "AMD Radeon 780M", "Lenovo", 999.5, "AMD Ryzen 7 7840HS", 16 },
                    { 4, "NVIDIA RTX 4080", "ASUS", 2199.0, "Intel Core i9-13900H", 32 },
                    { 5, "Intel Iris Xe", "HP", 649.0, "Intel Core i5-1335U", 8 },
                    { 6, "AMD Radeon 610M", "Acer", 499.0, "AMD Ryzen 5 7520U", 8 },
                    { 7, "NVIDIA RTX 4070", "MSI", 1899.99, "Intel Core i7-14700HX", 32 },
                    { 8, "Apple M3 10-core", "Apple", 1299.0, "M3", 8 },
                    { 9, "Intel Arc Graphics", "Lenovo", 1649.0, "Intel Core Ultra 7 155H", 32 },
                    { 10, "NVIDIA RTX 4090", "ASUS", 3299.0, "AMD Ryzen 9 7945HX", 64 },
                    { 11, "AMD Radeon Graphics", "HP", 849.0, "AMD Ryzen 7 7730U", 16 },
                    { 12, "Intel UHD Graphics", "Dell", 579.0, "Intel Core i5-1235U", 8 },
                    { 13, "NVIDIA RTX 4090", "Razer", 3599.0, "Intel Core i9-14900HX", 32 },
                    { 14, "Intel Arc Graphics", "Samsung", 1199.0, "Intel Core Ultra 5 125H", 16 },
                    { 15, "Intel Iris Xe", "Microsoft", 1499.0, "Intel Core i7-1255U", 16 },
                    { 16, "NVIDIA RTX 4050", "Acer", 1099.0, "Intel Core i7-13620H", 16 },
                    { 17, "NVIDIA RTX 4060", "Gigabyte", 1399.0, "Intel Core i7-13700H", 16 },
                    { 18, "Apple M3 Max 30-core", "Apple", 3999.0, "M3 Max", 36 },
                    { 19, "AMD Radeon 610M", "Lenovo", 429.0, "AMD Ryzen 3 7320U", 8 },
                    { 20, "Intel Arc Graphics", "HP", 1349.0, "Intel Core Ultra 7 155H", 16 }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Beschrijving", "Naam", "Prijs" },
                values: new object[,]
                {
                    { 1, "Dit is een fiets", "fiets", 100.00m },
                    { 2, "Dit is een mooie koersfiets", "koersfiets", 200.00m },
                    { 3, "Dit is een auto", "auto", 2000.00m }
                });

            migrationBuilder.InsertData(
                table: "Bestelling",
                columns: new[] { "Id", "KlantId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 2 },
                    { 5, 3 }
                });

            migrationBuilder.InsertData(
                table: "OrderLijn",
                columns: new[] { "Id", "Aantal", "BestellingId", "ProductId" },
                values: new object[,]
                {
                    { 1, 3.0, 1, 1 },
                    { 2, 7.0, 1, 2 },
                    { 3, 4.0, 2, 1 },
                    { 4, 1.0, 2, 2 },
                    { 5, 2.0, 3, 1 },
                    { 6, 3.0, 4, 1 },
                    { 7, 1.0, 4, 3 },
                    { 8, 2.0, 5, 1 },
                    { 9, 6.0, 5, 2 },
                    { 10, 10.0, 5, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bestelling_KlantId",
                table: "Bestelling",
                column: "KlantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLijn_BestellingId",
                table: "OrderLijn",
                column: "BestellingId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLijn_ProductId",
                table: "OrderLijn",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Laptops");

            migrationBuilder.DropTable(
                name: "OrderLijn");

            migrationBuilder.DropTable(
                name: "Bestelling");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Klant");
        }
    }
}
