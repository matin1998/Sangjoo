using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sangjoo.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveInventoryUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InventoryUnit",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "InventoryMin",
                table: "Products",
                type: "int",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InventoryMax",
                table: "Products",
                type: "int",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "InventoryMin",
                table: "Products",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "InventoryMax",
                table: "Products",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InventoryUnit",
                table: "Products",
                type: "int",
                nullable: true);
        }
    }
}
