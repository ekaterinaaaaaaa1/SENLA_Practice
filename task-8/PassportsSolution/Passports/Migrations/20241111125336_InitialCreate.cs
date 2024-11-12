using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Passports.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inactivepassports",
                columns: table => new
                {
                    passp_series = table.Column<short>(type: "smallint", nullable: false),
                    passp_number = table.Column<int>(type: "integer", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inactivepassports", x => new { x.passp_series, x.passp_number });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inactivepassports");
        }
    }
}
