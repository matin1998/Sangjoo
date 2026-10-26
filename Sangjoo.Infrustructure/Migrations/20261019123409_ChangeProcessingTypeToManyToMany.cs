using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sangjoo.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProcessingTypeToManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProcessingTypes_ProcessingTypeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProcessingTypeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProcessingTypeId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "StoneProcessingTypesMap",
                columns: table => new
                {
                    ProcessingTypesId = table.Column<long>(type: "bigint", nullable: false),
                    StonesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoneProcessingTypesMap", x => new { x.ProcessingTypesId, x.StonesId });
                    table.ForeignKey(
                        name: "FK_StoneProcessingTypesMap_ProcessingTypes_ProcessingTypesId",
                        column: x => x.ProcessingTypesId,
                        principalTable: "ProcessingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoneProcessingTypesMap_Products_StonesId",
                        column: x => x.StonesId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoneProcessingTypesMap_StonesId",
                table: "StoneProcessingTypesMap",
                column: "StonesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoneProcessingTypesMap");

            migrationBuilder.AddColumn<long>(
                name: "ProcessingTypeId",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProcessingTypeId",
                table: "Products",
                column: "ProcessingTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProcessingTypes_ProcessingTypeId",
                table: "Products",
                column: "ProcessingTypeId",
                principalTable: "ProcessingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
