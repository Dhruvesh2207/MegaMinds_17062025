using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MegaMinds_Practical.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Observations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObservationData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObservationId = table.Column<int>(type: "int", nullable: false),
                    SamplingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ObservationsModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservationData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObservationData_Observations_ObservationsModelId",
                        column: x => x.ObservationsModelId,
                        principalTable: "Observations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ObservationProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObservationDataId = table.Column<int>(type: "int", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObservationDataModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservationProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObservationProperties_ObservationData_ObservationDataModelId",
                        column: x => x.ObservationDataModelId,
                        principalTable: "ObservationData",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObservationData_ObservationsModelId",
                table: "ObservationData",
                column: "ObservationsModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ObservationProperties_ObservationDataModelId",
                table: "ObservationProperties",
                column: "ObservationDataModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObservationProperties");

            migrationBuilder.DropTable(
                name: "ObservationData");

            migrationBuilder.DropTable(
                name: "Observations");
        }
    }
}
