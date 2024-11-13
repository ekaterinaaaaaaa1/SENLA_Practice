﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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

            migrationBuilder.CreateTable(
                name: "passporthistory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    passp_series = table.Column<short>(type: "smallint", nullable: false),
                    passp_number = table.Column<int>(type: "integer", nullable: false),
                    active_start = table.Column<DateTime>(type: "date", nullable: false),
                    active_end = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passporthistory", x => x.id);
                    table.ForeignKey(
                        name: "FK_passporthistory_inactivepassports_passp_series_passp_number",
                        columns: x => new { x.passp_series, x.passp_number },
                        principalTable: "inactivepassports",
                        principalColumns: new[] { "passp_series", "passp_number" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_passporthistory_passp_series_passp_number",
                table: "passporthistory",
                columns: new[] { "passp_series", "passp_number" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "passporthistory");

            migrationBuilder.DropTable(
                name: "inactivepassports");
        }
    }
}
