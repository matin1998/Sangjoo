using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sangjoo.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceDimensionWithStoneDimensions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Dimensions_DimensionId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Dimensions");

            migrationBuilder.DropIndex(
                name: "IX_Products_DimensionId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DimensionId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "MaxLength",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxWidth",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinLength",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinWidth",
                table: "Products",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxLength",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MaxWidth",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MinLength",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MinWidth",
                table: "Products");

            migrationBuilder.AddColumn<long>(
                name: "DimensionId",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Dimensions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Length = table.Column<decimal>(type: "decimal(10,1)", precision: 10, scale: 1, nullable: false),
                    Width = table.Column<decimal>(type: "decimal(10,1)", precision: 10, scale: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimensions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_DimensionId",
                table: "Products",
                column: "DimensionId");

            migrationBuilder.CreateIndex(
                name: "IX_Dimensions_Length_Width",
                table: "Dimensions",
                columns: new[] { "Length", "Width" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Dimensions_DimensionId",
                table: "Products",
                column: "DimensionId",
                principalTable: "Dimensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
