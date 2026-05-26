using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApiDemo.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Laptops");
        }
    }
}
