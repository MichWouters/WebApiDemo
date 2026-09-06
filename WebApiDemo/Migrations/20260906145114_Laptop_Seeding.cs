using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiDemo.Migrations
{
    /// <inheritdoc />
    public partial class Laptop_Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Laptops",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
