using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiDemo.Migrations
{
    /// <inheritdoc />
    public partial class seed_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestelling_Klant_KlantId",
                table: "Bestelling");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLijn_Bestelling_BestellingId",
                table: "OrderLijn");

            migrationBuilder.InsertData(
                table: "Klant",
                columns: new[] { "Id", "AangemaaktDatum", "Naam", "Voornaam" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Van Der Neffe", "Leon" },
                    { 2, new DateTime(2022, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Van De Kasseinen", "Firmin" },
                    { 3, new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kiekeboe", "Marcel" }
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

            migrationBuilder.AddForeignKey(
                name: "FK_Bestelling_Klant_KlantId",
                table: "Bestelling",
                column: "KlantId",
                principalTable: "Klant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLijn_Bestelling_BestellingId",
                table: "OrderLijn",
                column: "BestellingId",
                principalTable: "Bestelling",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestelling_Klant_KlantId",
                table: "Bestelling");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLijn_Bestelling_BestellingId",
                table: "OrderLijn");

            migrationBuilder.DeleteData(
                table: "OrderLijn",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderLijn",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderLijn",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderLijn",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderLijn",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrderLijn",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OrderLijn",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "OrderLijn",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "OrderLijn",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "OrderLijn",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Bestelling",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bestelling",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bestelling",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bestelling",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Bestelling",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Klant",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Klant",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Klant",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddForeignKey(
                name: "FK_Bestelling_Klant_KlantId",
                table: "Bestelling",
                column: "KlantId",
                principalTable: "Klant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLijn_Bestelling_BestellingId",
                table: "OrderLijn",
                column: "BestellingId",
                principalTable: "Bestelling",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
