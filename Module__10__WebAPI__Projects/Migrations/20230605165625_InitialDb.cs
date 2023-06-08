using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Module__10__WebAPI__Projects.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarDetails",
                columns: table => new
                {
                    CarDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LaunchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    IsStock = table.Column<bool>(type: "bit", nullable: false),
                    CarModel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarDetails", x => x.CarDetailId);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDetails",
                columns: table => new
                {
                    CompanyDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyRank = table.Column<int>(type: "int", nullable: false),
                    CompanyInformation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CarDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDetails", x => x.CompanyDetailId);
                    table.ForeignKey(
                        name: "FK_CompanyDetails_CarDetails_CarDetailId",
                        column: x => x.CarDetailId,
                        principalTable: "CarDetails",
                        principalColumn: "CarDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartDetails",
                columns: table => new
                {
                    PartDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartsName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PartsPrice = table.Column<decimal>(type: "money", nullable: false),
                    PartsMFG = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartDetails", x => x.PartDetailId);
                    table.ForeignKey(
                        name: "FK_PartDetails_CarDetails_CarDetailId",
                        column: x => x.CarDetailId,
                        principalTable: "CarDetails",
                        principalColumn: "CarDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDetails_CarDetailId",
                table: "CompanyDetails",
                column: "CarDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PartDetails_CarDetailId",
                table: "PartDetails",
                column: "CarDetailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyDetails");

            migrationBuilder.DropTable(
                name: "PartDetails");

            migrationBuilder.DropTable(
                name: "CarDetails");
        }
    }
}
